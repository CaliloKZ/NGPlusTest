using System;
using System.Collections.Generic;
using Grid;
using SaveSystemCore.SaveData;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class InventoryClass
    {
        public event Action<InventorySlot> OnSlotSetup;
        public UIGrid<InventorySlot> GetGrid() { return _inventoryGrid; }
        UIGrid<InventorySlot> _inventoryGrid;
        int _width;
        int _height;
        
        int _slotCreationIndex = 0;
        
        public void CreateGrid(GridSettings_SO inventorySettings)
        {
            _width = inventorySettings.width;
            _height = inventorySettings.height;
            
            _inventoryGrid = new UIGrid<InventorySlot>(
                width: _width,
                height: _height,
                createGridObject: CreateInventorySlot
            );
        }

        InventorySlot CreateInventorySlot(UIGrid<InventorySlot> grid, int x, int y)
        {
            InventorySlot slot = new InventorySlot();
            slot.SetSlotIndex(_slotCreationIndex);
            _slotCreationIndex++;
            OnSlotSetup?.Invoke(slot);
            return slot;
        }

        public bool TryAddItem(Item_SO item, int amount, out int remainingAmount)
        {
            InventorySlot firstEmptySlot = null;
            int width = _width;
            int height = _height;
            int remaining = amount;
            
            for (int y = 0; y < height && remaining > 0; y++)
            {
                for (int x = 0; x < width && remaining > 0; x++)
                {
                    InventorySlot slot = _inventoryGrid.GetValue(x, y);
                    if (slot.TryGetItemData(out _))
                    {
                        if (!slot.CanStack(item.itemID)) 
                            continue;
                        
                        remaining -= slot.StackItem(item, remaining);
                    }
                    else if (null == firstEmptySlot)
                    {
                        firstEmptySlot = slot;
                    }
                }
            }

            if (null != firstEmptySlot && remaining > 0)
            {
                int addAmount = Mathf.Min(item.maxStackSize, remaining);
                firstEmptySlot.SetItem(item, addAmount);
                remaining -= addAmount;
            }

            remainingAmount = remaining;
            return remaining <= 0;
        }

        public void DropItem(InventorySlot slot)
        {
            slot.ClearSlot();
        }

        public void SetSelectedSlot(InventorySlot selectedSlot)
        {
            int width = _width;
            int height = _height;
            
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    InventorySlot slot = _inventoryGrid.GetValue(x, y);
                    slot.SetSelected(slot == selectedSlot);
                }
            }
        }
        
        public void SwapSlots(InventorySlot slotA, InventorySlot slotB)
        {
            Item_SO tempItem = slotA.ItemData;
            int tempStack = slotA.StackSize;

            slotA.SetItem(slotB.ItemData, slotB.StackSize);
            slotB.SetItem(tempItem, tempStack);

            SetSelectedSlot(slotB);
        }

        public InventorySlot[] GetAllSlots()
        {
            List<InventorySlot> slotList = new();
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    slotList.Add(_inventoryGrid.GetValue(x, y));
                }
            }
            
            return slotList.ToArray();
        }

        public void LoadSlots(InventorySaveData saveData, ref List<ScriptableObject> itemDatas)
        {
            InventorySlot[] slotArray = GetAllSlots();
            int maxSlots = Mathf.Min(slotArray.Length, saveData.slots.Count);
            
            for (int i = 0; i < maxSlots; i++)
            {
                SlotSaveData slotData = saveData.slots[i];

                if (slotData.ItemID < 0 || slotData.ItemID >= itemDatas.Count)
                    continue;

                if (itemDatas[slotData.ItemID] is not Item_SO itemData)
                    continue;

                slotArray[i].SetItem(itemData, slotData.StackSize);
            }
        }
    }
}