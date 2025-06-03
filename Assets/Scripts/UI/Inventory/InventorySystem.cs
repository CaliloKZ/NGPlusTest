using System;
using System.Collections.Generic;
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
    
        [SerializeField] List<InventorySlot> inventorySlots = new();
        [SerializeField] ItemDragHandler dragItem;
        [SerializeField] ItemDescriptionUI itemDescriptionUI;
        [SerializeField] Item_SO testItemData;
        [SerializeField] Item_SO testItemData2;
        [SerializeField] Assetbook_SO itemBook;
    
        Dictionary<int, InventorySlot> slotsInstanceDictionary;
        InventorySlot _selectedSlot;

    

        void Awake()
        {
            slotsInstanceDictionary = new Dictionary<int, InventorySlot>();
            foreach (var slot in inventorySlots)
            {
                slotsInstanceDictionary.Add(slot.gameObject.GetInstanceID(), slot);
            }
            DragItem = dragItem;
            Test();
        }

        void Start()
        {
            OnItemDropAction += OnItemDrop;
            OnItemSelectAction += OnItemSelected;
        }

        void OnDestroy()
        {
            OnItemDropAction -= OnItemDrop;
            OnItemSelectAction -= OnItemSelected;
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
                inventorySlots[slotItemPair.Key].SetItemData(itemData);
            }
        }

        void Test()
        {
            inventorySlots[0].SetItemData(testItemData);
            inventorySlots[3].SetItemData(testItemData2);
        }

        void OnItemDrop(InventorySlot oldSlot, int newSlotID)
        {
            if (!slotsInstanceDictionary.TryGetValue(newSlotID, out var newSlot)) 
                return;

            Item_SO oldSlotItemData = oldSlot.ItemData;

            bool newSlotHasItemData = newSlot.TryGetItemData(out Item_SO itemData);
        
            newSlot.SetItemData(oldSlotItemData);
            OnItemSelected(newSlot);

            if (newSlotHasItemData)
            {
                oldSlot.SetItemData(itemData);
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
    }
}
