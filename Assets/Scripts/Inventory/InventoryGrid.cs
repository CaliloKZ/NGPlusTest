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
            _inventoryGrid = new UIGrid<InventorySlot>(
                width: InventorySettings.width,
                height: InventorySettings.height,
                cellSize: InventorySettings.cellSize,
                originPosition: InventorySettings.originPosition,
                createGridObject: (grid, x, y) => new InventorySlot()
            );
        }
    }
}