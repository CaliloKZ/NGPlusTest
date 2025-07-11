using System;
using System.Collections.Generic;
using Inventory;

namespace SaveSystemCore.SaveData
{
    [Serializable]
    public class InventorySaveData
    {
        public List<SlotSaveData> slots;
        public InventorySaveData(InventoryClass data)
        {
            slots = new List<SlotSaveData>();
            InventorySlot[] slotArray = data.GetAllSlots();
            for (int i = 0; i < slotArray.Length; i++)
            {
                slots.Add(new SlotSaveData
                {
                    ItemID = slotArray[i].TryGetItemData(out Item_SO itemData) ? itemData.itemID : -1,
                    StackSize = slotArray[i].StackSize,
                });
            }
        }
    }

    [Serializable]
    public class SlotSaveData
    {
        public int ItemID;
        public int StackSize;
    }
}