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
        Dictionary<int, EquipItems> _equippedItemsDictionary;
        
        EquipItems _currentEquippedItem;

        void Awake()
        {
            _equippedItemsDictionary = new Dictionary<int, EquipItems>();
            foreach (var item in equipItemsList)
            {
                _equippedItemsDictionary.Add(item.ItemData.itemID, item);
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
            
            _currentEquippedItem = _equippedItemsDictionary[itemData.itemID];
            _currentEquippedItem.OnItemEquipped(itemData);
        }
    }
}
