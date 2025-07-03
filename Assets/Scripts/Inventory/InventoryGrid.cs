using System;
using GridSystem;
using UnityEngine;

namespace Inventory
{
    [Serializable]
    public class InventoryGrid
    {
        [field: SerializeField] public GridSettings_SO InventorySettings { get; private set; }
        
        UIGrid<InventorySlot> _inventoryGrid;
        public UIGrid<InventorySlot> GetGrid() { return _inventoryGrid; }
        
        public void CreateGrid()
        {
            if (null == InventorySettings)
            {
                Debug.LogError("InventorySettings is not assigned.");
                return;
            }

            
            _inventoryGrid = new UIGrid<InventorySlot>(
                width: InventorySettings.width,
                height: InventorySettings.height,
                createGridObject: (grid, x, y) => new InventorySlot()
            );
        }

        public bool TryAddItem(Item_SO item, int amount, out int remainingAmount)
        {
            InventorySlot firstEmptySlot = null;
            int width = InventorySettings.width;
            int height = InventorySettings.height;
            int remaining = amount;
            
            for (int x = 0; x < width && remaining > 0; x++)
            {
                for (int y = 0; y < height && remaining > 0; y++)
                {
                    var slot = _inventoryGrid.GetValue(x, y);
                    if (slot.TryGetItemData(out _))
                    {
                        if (slot.CanStack(item.itemID))
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
    }
}