using System;
using UnityEngine;

namespace Inventory
{
    public class InventorySlot
    {
        public event Action OnSlotChanged;
        public Item_SO ItemData { get; private set; } 
        public int StackSize { get; private set; } 
        public int SlotIndex { get; private set; }
        public bool IsSelected { get; private set; }

        public bool CanStack(int itemID)
        {
            return null != ItemData
                   && itemID == ItemData.itemID 
                   && ItemData.isStackable 
                   && StackSize < ItemData.maxStackSize;    
        }
    
        public void ClearSlot()
        {
            ItemData = null;
            StackSize = 0;
            IsSelected = false;
            OnSlotChanged?.Invoke();
        }
    
        public void SetItem(Item_SO newItem, int newAmount = 1)
        {
            ItemData = newItem;
            StackSize = newAmount;
            OnSlotChanged?.Invoke();
        }
    
        public bool TryGetItemData(out Item_SO itemDataAsset)
        {
            itemDataAsset = ItemData;
            return ItemData != null;
        }
        
        public int StackItem(Item_SO item, int amount)
        {
            if (!CanStack(item.itemID)) 
                return 0;

            int spaceLeft = ItemData.maxStackSize - StackSize;
            int amountToAdd = Mathf.Min(spaceLeft, amount);
            StackSize += amountToAdd;
            OnSlotChanged?.Invoke();
            return amountToAdd;
        }

        public void SetSlotIndex(int slotIndex)
        {
            SlotIndex = slotIndex;
        }

        public void SetSelected(bool isSelected)
        {
            IsSelected = isSelected;
            OnSlotChanged?.Invoke();
        }
        
    }
}