using System.Collections.Generic;
using GameEvents;
using Inventory;
using Items;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerEquipmentController : MonoBehaviour, IGameEventListener<ScriptableObject>
    {
        [SerializeField] GameEvent<ScriptableObject> onItemEquipped;
        [SerializeField] GameEvent<ScriptableObject> onItemUsed;
        [SerializeField] InputActionReference fireActionReference;
        [SerializeField] List<EquipItems> equipItemsList = new();
        
        EquipItems _currentEquippedItem;
        
        void Start()
        {
            onItemEquipped.RegisterListener(this);
            onItemUsed.RegisterListener(this);
        }

        void OnDestroy()
        {
            onItemEquipped.UnregisterListener(this);
            onItemUsed.UnregisterListener(this);
        }
        
        public void OnEventRaised(ScriptableObject source)
        {
            if (null != _currentEquippedItem)
            {
                _currentEquippedItem.OnItemUnequipped();
                _currentEquippedItem = null;
            }
            
            if(null == source || 
               source is not Item_SO itemData)
                return;

            EquipItem(itemData);
        }

        void EquipItem(Item_SO itemData)
        {
            _currentEquippedItem = equipItemsList[itemData.itemID];
            _currentEquippedItem.OnItemEquipped(itemData);
        }
    }
}
