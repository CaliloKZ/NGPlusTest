using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "InventorySlot_SO", menuName = "Scriptable Objects/InventorySlot_SO")]
    public class InventorySlot_SO : ScriptableObject
    {
        public Item_SO itemData; 
        public int stackSize;

        public bool CanStack(int itemID)
        {
            return null != itemData
                   && itemID == itemData.itemID 
                   && itemData.isStackable 
                   && stackSize < itemData.maxStackSize;    
        }
    
        public void ClearSlot()
        {
            itemData = null;
            stackSize = 0;
        }
    
        public void ChangeItemAmount(int amountDiffer)
        {
            stackSize += amountDiffer;
        }
    
        public void SetItem(Item_SO newItem, int newAmount = 1)
        {
            itemData = newItem;
            stackSize = newAmount;
        }
    
        public bool TryGetItemData(out Item_SO itemDataAsset)
        {
            itemDataAsset = itemData;
            return itemData != null;
        }
    }
}
