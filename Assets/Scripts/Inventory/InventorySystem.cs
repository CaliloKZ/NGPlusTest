using System.Collections.Generic;
using GameEvents;
using Inventory.UI;
using Items;
using SaveSystemCore;
using SaveSystemCore.SaveData;
using UnityEngine;

namespace Inventory
{
    public class InventorySystem : MonoBehaviour, IGameEventListener<MonoBehaviour>, IGameEventListener<InventorySlot>, IGameEventListener<InventorySlot, GameObject>
    {
        const string SAVE_FILE_NAME = "Inventory.json";
        
        [SerializeField] InventoryUI inventoryUI;
        [SerializeField] GridSettings_SO inventorySettings; 
        [SerializeField] Assetbook_SO itemsBook; 
        
        [SerializeField] GameEvent<ScriptableObject> onItemEquipped;
        [SerializeField] GameEvent<MonoBehaviour> itemCollectEvent;
        [SerializeField] GameEvent<InventorySlot> onItemDroppedEvent;
        [SerializeField] GameEvent<InventorySlot> onSlotSelectedEvent;
        [SerializeField] GameEvent<InventorySlot, GameObject> onSlotSwapEvent;
        
        readonly InventoryClass _inventoryClass = new();

        void Awake()
        {
            itemCollectEvent.RegisterListener(this);
            onItemDroppedEvent.RegisterListener(this);
            onSlotSelectedEvent.RegisterListener(this);
            onSlotSwapEvent.RegisterListener(this);
            
            _inventoryClass.OnSlotSetup += SetupInventoryClassSlotUI;
            _inventoryClass.CreateGrid(inventorySettings);
            LoadInventory();
        }

        private void OnDestroy()
        {
            itemCollectEvent.UnregisterListener(this);
            onItemDroppedEvent.UnregisterListener(this);
            onSlotSelectedEvent.UnregisterListener(this);
            onSlotSwapEvent.UnregisterListener(this);
            
            _inventoryClass.OnSlotSetup -= SetupInventoryClassSlotUI;
        }

        void SetupInventoryClassSlotUI(InventorySlot slot)
        {
            inventoryUI.SlotSetup(slot);
        }
        
        public void OnEventRaised(MonoBehaviour source)
        {
            switch (source)
            {
                case ItemCollectable itemCollectable:
                    OnItemCollected(itemCollectable);
                    break;
                default:
                    break;
            }
        }

        public void OnEventRaised(InventorySlot slot)
        {
            _inventoryClass.SetSelectedSlot(slot);
            inventoryUI.ItemSelected(slot.ItemData);
            onItemEquipped.Raise(slot.ItemData);
        }
        
        public void OnEventRaised(InventorySlot slot, GameObject pointerEnter)
        {
            if (pointerEnter.TryGetComponent(out InventorySlotUI slotUI))
            {
                _inventoryClass.SwapSlots(slot, slotUI.Slot);
                SaveInventory();
                return;
            }

            if (!pointerEnter.transform.parent.TryGetComponent(out InventoryUI _)) 
                return;
            
            OnItemDrop(slot);
            SaveInventory();
        }

        void OnItemCollected(ItemCollectable item)
        {
            if (!_inventoryClass.TryAddItem(item.itemData, item.Amount, out int remainingAmount))
            {
                item.SetItemAmount(remainingAmount);
                SaveInventory();
                return;
            }
            
            item.ItemCollected();
            SaveInventory();
        }

        void OnItemDrop(InventorySlot slot)
        {
            onItemDroppedEvent.Raise(slot);
            _inventoryClass.DropItem(slot);
            SaveInventory();
        }

        void SaveInventory()
        {
            InventorySaveData saveData = new InventorySaveData(_inventoryClass);
            SaveSystem.Save(saveData, SAVE_FILE_NAME);
        }

        void LoadInventory()
        {
            InventorySaveData saveData = SaveSystem.Load<InventorySaveData>(SAVE_FILE_NAME);
            if (saveData == null || saveData.slots == null)
            {
                Debug.LogWarning("No save data found.");
                return;
            }
            List<ScriptableObject> itemList = itemsBook.assetList;
            _inventoryClass.LoadSlots(saveData, ref itemList);
        }


        public void ClearInventorySave()
        {
            SaveSystem.Delete(SAVE_FILE_NAME);
        }
    }
}
