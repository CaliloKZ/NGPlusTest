using System;
using System.Collections.Generic;
using Inventory;
using Items;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerEquipmentController : MonoBehaviour
    {
        [SerializeField] InputActionReference fireActionReference;
        [SerializeField] List<EquipItems> equipItemsList = new();
        
        EquipItems _currentEquippedItem;
        
        void Start()
        {
            InventorySystem.OnItemEquipAction += EquipItem;
            InventorySystem.OnItemUnequipAction += UnequipItem;
        }

        void OnDestroy()
        {
            InventorySystem.OnItemEquipAction -= EquipItem;
            InventorySystem.OnItemUnequipAction -= UnequipItem;
        }

        void EquipItem(InventorySlot_SO slotData)
        {
            if (null != _currentEquippedItem)
                _currentEquippedItem.OnItemUnequipped();
            
            _currentEquippedItem = equipItemsList[slotData.itemData.itemID];
            _currentEquippedItem.OnItemEquipped(slotData.itemData);
        }
        
        void UnequipItem()
        {
            if (null == _currentEquippedItem)
                return;
            
            _currentEquippedItem.OnItemUnequipped();
            _currentEquippedItem = null;
        }
    }
}
