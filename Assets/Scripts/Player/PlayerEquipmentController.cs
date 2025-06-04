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

        void Awake()
        {
            foreach (var item in equipItemsList)
            {
                item.SetFireActionReference(fireActionReference);
            }
        }

        void Start()
        {
            InventorySystem.OnItemEquipAction += EquipItem;
        }

        void OnDestroy()
        {
            InventorySystem.OnItemEquipAction -= EquipItem;
        }

        void EquipItem(Item_SO itemData)
        {
            if (null != _currentEquippedItem)
                _currentEquippedItem.OnItemUnequipped();
            
            _currentEquippedItem = equipItemsList[itemData.itemID];
            _currentEquippedItem.OnItemEquipped(itemData);
        }
    }
}
