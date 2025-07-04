using System;
using System.Collections.Generic;
using Grid;
using UnityEngine;
using UnityEngine.Serialization;

namespace Inventory
{
    [Serializable]
    public class Inventory
    {
        public event Action<InventorySlot> OnSlotChanged;
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
            return slot;
        }

        public bool TryAddItem(Item_SO item, int amount, out int remainingAmount)
        {
            InventorySlot firstEmptySlot = null;
            int width = _width;
            int height = _height;
            int remaining = amount;
            
            for (int x = 0; x < width && remaining > 0; x++)
            {
                for (int y = 0; y < height && remaining > 0; y++)
                {
                    InventorySlot slot = _inventoryGrid.GetValue(x, y);
                    if (slot.TryGetItemData(out _))
                    {
                        if (!slot.CanStack(item.itemID)) 
                            continue;
                        
                        remaining -= slot.StackItem(item, remaining);
                        OnSlotChanged?.Invoke(slot);
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
                OnSlotChanged?.Invoke(firstEmptySlot);
                remaining -= addAmount;
            }

            remainingAmount = remaining;
            return remaining <= 0;
        }

        public void DropItem(InventorySlot slot)
        {
            slot.ClearSlot();
            OnSlotChanged?.Invoke(slot);
        }
    }
}