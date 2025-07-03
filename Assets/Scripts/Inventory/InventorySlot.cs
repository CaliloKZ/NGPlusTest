using UnityEngine;

namespace Inventory
{
    public class InventorySlot
    {
        public Item_SO ItemData { get; private set; } 
        public int StackSize { get; private set; } 
        
        bool _slotChanged = false;

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
        }
    
        public void SetItem(Item_SO newItem, int newAmount = 1)
        {
            ItemData = newItem;
            StackSize = newAmount;
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
            return amountToAdd;
        }
    }
}