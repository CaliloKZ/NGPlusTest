using System;
using System.Collections.Generic;
using System.Linq;
using GameEvents;
using Items;
using Player;
using Pool;
using SaveSystem;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Inventory
{
    public class InventorySystem : MonoBehaviour, IGameEventListener<ItemCollectable>
    {
        [SerializeField] GameEvent<ItemCollectable> itemCollectEvent;
        [SerializeField] InventoryGrid inventoryGrid = new();

        private void Awake()
        {
            itemCollectEvent.RegisterListener(this);
            inventoryGrid.CreateGrid();
        }
        
        public void OnEventRaised(ItemCollectable source)
        {
            Debug.Log($"InventorySystem.OnItemCollectable: {source.itemData.name}");
            
            if (!inventoryGrid.TryAddItem(source.itemData, source.Amount, out int remainingAmount))
            {
                source.SetItemAmount(remainingAmount);
                return;
            }
            
            source.ItemCollected();
            UpdateInventoryUI();
        }

        void UpdateInventoryUI()
        {
            
        }

        #region old

        //     public static ItemDragHandler DragItem { get; private set; }
        //     public static InventorySlot_SO EquippedItemSlot { get; private set; }
        //
        //     public static Action<InventorySlotUI> OnBeginItemDragAction;
        //     public static Action OnEndItemDragAction;
        //     public static Action<InventorySlotUI, int> OnItemDropAction;
        //     
        //     public static Action OnItemUseAction;
        //     public static Action<InventorySlotUI> OnItemSelectAction;
        //     public static Action<ItemCollectable> OnItemPickupAction; 
        //     public static Action<InventorySlot_SO> OnItemEquipAction; 
        //     public static Action OnItemUnequipAction; 
        //     
        //     public static Action<InventorySlot_SO> OnSlotUpdateAction; 
        //     
        //     [SerializeField] List<InventorySlotUI> inventorySlots = new();
        //     [SerializeField] List<HotBarSlot> hotBarSlots = new();
        //     [SerializeField] GameObject inventoryPanel;
        //     [SerializeField] Image equippedItemIconImage;
        //     
        //     [SerializeField] ItemDragHandler dragItem;
        //     [SerializeField] ItemDescriptionUI itemDescriptionUI;
        //     [SerializeField] Assetbook_SO itemBook;
        //
        //     [SerializeField] float minDropDistance = 0.5f;
        //     [SerializeField] float maxDropDistance = 1.5f;
        //
        //     Dictionary<int, InventorySlotUI> _slotsInstanceDictionary;
        //     InventorySlotUI _selectedSlotUI;
        //     
        //     void Awake()
        //     {
        //         _slotsInstanceDictionary = new Dictionary<int, InventorySlotUI>();
        //         foreach (var slot in inventorySlots)
        //         {
        //             _slotsInstanceDictionary.Add(slot.gameObject.GetInstanceID(), slot);
        //         }
        //         DragItem = dragItem;
        //     }
        //
        //     void Start()
        //     {
        //         OnItemDropAction += OnItemDrop;
        //         OnItemSelectAction += OnItemSelected;
        //         OnItemPickupAction += OnItemPickup;
        //         OnItemEquipAction += OnEquipItem;
        //         OnItemUnequipAction += OnUnequipItem;
        //         OnItemUseAction += OnItemUsed;
        //         SaveController.StartLoadGame();
        //     }
        //
        //     void OnDestroy()
        //     {
        //         SaveController.StartSaveGame();
        //         OnItemDropAction -= OnItemDrop;
        //         OnItemSelectAction -= OnItemSelected;
        //         OnItemPickupAction -= OnItemPickup;
        //         OnItemEquipAction -= OnEquipItem;
        //         OnItemUnequipAction -= OnUnequipItem;
        //         OnItemUseAction -= OnItemUsed;
        //     }
        //
        //     void OnItemPickup(ItemCollectable item)
        //     {
        //         Item_SO itemData = item.itemData;
        //         
        //         if (!itemData.isStackable
        //             || !TryGetStackOrEmptySlot(itemData.itemID, out InventorySlotUI targetSlot))
        //         {
        //             targetSlot = inventorySlots.Find(slot => !slot.SlotData.TryGetItemData(out _));
        //             if (targetSlot == null)
        //                 return;
        //         }
        //         
        //         int newStackSize = targetSlot.SlotData.stackSize + item.amount;
        //         if (newStackSize > itemData.maxStackSize)
        //         {
        //             item.amount = newStackSize - itemData.maxStackSize;
        //             targetSlot.SetItemData(itemData, itemData.maxStackSize);
        //             OnItemPickup(item);
        //         }
        //         else
        //         {
        //             targetSlot.SetItemData(itemData, newStackSize);
        //             FastPool.Destroy(item);
        //         }
        //         OnSlotUpdateAction?.Invoke(targetSlot.SlotData);
        //         SaveInventory();
        //     }
        //
        //     bool TryGetStackOrEmptySlot(int itemID, out InventorySlotUI stackSlotUI)
        //     {
        //         stackSlotUI = inventorySlots.Find(slot => slot.SlotData.CanStack(itemID));
        //         return stackSlotUI != null;
        //     }
        //     
        //     public List<InventorySaveData> SaveInventory()
        //     {
        //         List<InventorySaveData> inventoryData = new();
        //         for (int i = 0; i < inventorySlots.Count; i++)
        //         {
        //             InventorySlot_SO slotData = inventorySlots[i].SlotData;
        //             if (!slotData.TryGetItemData(out Item_SO itemData))
        //                 continue;
        //
        //             InventorySaveData data = new InventorySaveData()
        //             {
        //                 ItemID = itemData.itemID,
        //                 SlotID = i,
        //                 StackSize = slotData.stackSize
        //             };
        //             inventoryData.Add(data);
        //         }
        //         return inventoryData;
        //     }
        //
        //     public void LoadInventory(SaveData saveData)
        //     {
        //         foreach (InventorySaveData data in saveData.inventorySaveData)
        //         {
        //             Item_SO itemData = itemBook.assetList[data.ItemID] as Item_SO;
        //             inventorySlots[data.SlotID].ClearSlot();
        //             inventorySlots[data.SlotID].SetItemData(itemData, data.StackSize);
        //         }
        //     }
        //
        //     void OnItemDrop(InventorySlotUI oldSlotUI, int newSlotID)
        //     {
        //         if(null == oldSlotUI || null == oldSlotUI.SlotData.itemData)
        //             return;
        //         
        //         if (newSlotID == inventoryPanel.GetInstanceID())
        //         {
        //             DropItem(oldSlotUI);
        //             return;
        //         }
        //         
        //         if (!_slotsInstanceDictionary.TryGetValue(newSlotID, out InventorySlotUI newSlot)) 
        //             return;
        //         
        //         InventorySlot_SO oldSlotData = oldSlotUI.SlotData;
        //         
        //         bool newSlotHasItemData = newSlot.SlotData.TryGetItemData(out Item_SO itemData);
        //         int newStackSize = newSlot.SlotData.stackSize;
        //         
        //         if (newSlotHasItemData && itemData == oldSlotData.itemData)
        //         {
        //             newStackSize = newSlot.SlotData.stackSize + oldSlotData.stackSize;
        //             if (newStackSize > itemData.maxStackSize)
        //             {
        //                 int overflowAmount = newStackSize - itemData.maxStackSize;
        //                 newSlot.SetItemData(itemData, itemData.maxStackSize);
        //                 oldSlotUI.SetItemData(itemData, overflowAmount);
        //             }
        //             else
        //             {
        //                 newSlot.SetItemData(itemData, newStackSize);
        //                 oldSlotUI.ClearSlot();
        //             }
        //             
        //             OnItemSelected(newSlot);
        //             return;
        //         }
        //         
        //         newSlot.SetItemData(oldSlotData.itemData, oldSlotData.stackSize);
        //         OnItemSelected(newSlot);
        //         
        //         if (newSlotHasItemData)
        //         {
        //             oldSlotUI.SetItemData(itemData, newStackSize);
        //             return;
        //         }
        //         
        //         oldSlotUI.ClearSlot();
        //     }
        //
        //     void OnItemSelected(InventorySlotUI slotUI)
        //     {
        //         if (null != _selectedSlotUI)
        //             _selectedSlotUI.ToggleItemSelectedBorder(false);
        //     
        //         _selectedSlotUI = slotUI;
        //         _selectedSlotUI.ToggleItemSelectedBorder(true);
        //     
        //         Item_SO itemData = slotUI.SlotData.itemData;
        //         itemDescriptionUI.SetItemDescription(itemData.itemIcon, itemData.itemName, itemData.itemDescription);
        //     }
        //
        //     void OnEquipItem(InventorySlot_SO slotData)
        //     {
        //         EquippedItemSlot = slotData;
        //         equippedItemIconImage.sprite = slotData.itemData.itemIcon;
        //         equippedItemIconImage.enabled = true;
        //         
        //         OnSlotUpdateAction?.Invoke(slotData);
        //     }
        //     
        //     void OnUnequipItem()
        //     {
        //         EquippedItemSlot = null;
        //         equippedItemIconImage.sprite = null;
        //         equippedItemIconImage.enabled = false;
        //     }
        //
        //     void DropItem(InventorySlotUI slotUI)
        //     {
        //         if(EquippedItemSlot == slotUI.SlotData)
        //         {
        //             OnItemUnequipAction?.Invoke();
        //         }
        //         
        //         Vector2 dropOffset = Random.insideUnitCircle.normalized * Random.Range(minDropDistance, maxDropDistance);
        //         Vector2 dropPosition = (Vector2)PlayerInputController.PlayerTransform.position + dropOffset;
        //         
        //         ItemCollectable item = FastPool.Instantiate<ItemCollectable>(slotUI.SlotData.itemData.prefabID, dropPosition, quaternion.identity);
        //         item.SetItemAmount(slotUI.SlotData.stackSize);
        //         
        //         slotUI.ClearSlot();
        //     }
        //
        //     public void ResetInventory()
        //     {
        //         foreach (InventorySlotUI slot in inventorySlots)
        //         {
        //             slot.ClearSlot();
        //         }
        //         
        //         foreach (HotBarSlot slot in hotBarSlots)
        //         {
        //             slot.ClearSlot();
        //         }
        //
        //         SaveInventory();
        //         
        //         if (null == EquippedItemSlot) 
        //             return;
        //         
        //         EquippedItemSlot = null;
        //         equippedItemIconImage.sprite = null;
        //         equippedItemIconImage.enabled = false;
        //             
        //         OnItemUnequipAction?.Invoke();
        //     }
        //
        //     void OnItemUsed()
        //     {
        //         if(null == EquippedItemSlot)
        //             return;
        //
        //         foreach (var slot in inventorySlots.Where(slot => slot.SlotData == EquippedItemSlot))
        //         {
        //             slot.OnItemUsed();
        //             break;
        //         }
        //     }
        // }

        #endregion
    }
}
