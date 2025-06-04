using System;
using System.Collections.Generic;
using Pool;
using SaveSystem;
using UnityEngine;

namespace UI.Inventory
{
    public class InventorySystem : MonoBehaviour
    {
        public static ItemDragHandler DragItem { get; private set; }

        public static Action<InventorySlot> OnBeginItemDragAction;
        public static Action OnEndItemDragAction;
        public static Action<InventorySlot, int> OnItemDropAction;
        
        public static Action<InventorySlot> OnItemSelectAction;
        public static Action<ItemCollectable> OnItemPickupAction; 
   
        
        [SerializeField] List<InventorySlot> inventorySlots = new();
        [SerializeField] ItemDragHandler dragItem;
        [SerializeField] ItemDescriptionUI itemDescriptionUI;
        [SerializeField] Assetbook_SO itemBook;
    
        Dictionary<int, InventorySlot> _slotsInstanceDictionary;
        InventorySlot _selectedSlot;
        
        void Awake()
        {
            _slotsInstanceDictionary = new Dictionary<int, InventorySlot>();
            foreach (var slot in inventorySlots)
            {
                _slotsInstanceDictionary.Add(slot.gameObject.GetInstanceID(), slot);
            }
            DragItem = dragItem;
        }

        void Start()
        {
            OnItemDropAction += OnItemDrop;
            OnItemSelectAction += OnItemSelected;
            OnItemPickupAction += OnItemPickup;
        }

        void OnDestroy()
        {
            OnItemDropAction -= OnItemDrop;
            OnItemSelectAction -= OnItemSelected;
            OnItemPickupAction -= OnItemPickup;
        }

        void OnItemPickup(ItemCollectable item)
        {
            for (var i = 0; i < inventorySlots.Count; i++)
            {
                InventorySlot slot = inventorySlots[i];
                if (slot.TryGetItemData(out _))
                    continue;

                SetItemData(slot, item.itemData);
                FastPool.Destroy(item.gameObject);
                return;
            }
        }

        void SetItemData(InventorySlot slot, Item_SO itemData)
        {
            slot.SetItemData(itemData);
        }
        
        public Dictionary<int, int> SaveInventory()
        {
            Dictionary<int, int> itemDictionary = new Dictionary<int, int>();
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if (!inventorySlots[i].TryGetItemData(out Item_SO itemData))
                    continue;
                
                itemDictionary.Add(i, itemData.itemID);
            }

            return itemDictionary;
        }

        public void LoadInventory(SaveData saveData)
        {
            foreach (var slotItemPair in saveData.InventoryData)
            {
                Item_SO itemData = itemBook.assetList[slotItemPair.Value] as Item_SO;
                SetItemData(inventorySlots[slotItemPair.Key], itemData);
            }
        }

        void OnItemDrop(InventorySlot oldSlot, int newSlotID)
        {
            if (!_slotsInstanceDictionary.TryGetValue(newSlotID, out var newSlot)) 
                return;

            Item_SO oldSlotItemData = oldSlot.ItemData;

            bool newSlotHasItemData = newSlot.TryGetItemData(out Item_SO itemData);
        
            SetItemData(newSlot, oldSlotItemData);
            OnItemSelected(newSlot);

            if (newSlotHasItemData)
            {
                SetItemData(oldSlot, itemData);
                return;
            }
        
            oldSlot.ResetSlot();
        }

        void OnItemSelected(InventorySlot slot)
        {
            if (null != _selectedSlot)
                _selectedSlot.ToggleItemSelectedBorder(false);
        
            _selectedSlot = slot;
            _selectedSlot.ToggleItemSelectedBorder(true);
        
            Item_SO itemData = slot.ItemData;
            itemDescriptionUI.SetItemDescription(itemData.itemIcon, itemData.itemName, itemData.itemDescription);
        }
        
        public void EquipItem(InventorySlot slot)
        {
            
        }
    }
}
