using System;
using System.Collections.Generic;
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
