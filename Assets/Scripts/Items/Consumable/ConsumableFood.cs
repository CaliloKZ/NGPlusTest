using GameEvents;
using Inventory;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Items.Consumable
{
    public class ConsumableFood : EquipItems
    {
        [SerializeField] GameEvent<ScriptableObject> onItemUsed;
        [SerializeField] SpriteRenderer itemRenderer;
        
        protected override void OnFireAction(InputAction.CallbackContext obj)
        {
            PlayerInputController.ChangePlayerState(PlayerState.UsingItem);
        }
        
        public override void OnPlayerStateChanged()
        {
            PlayerState newState = PlayerInputController.CurrentState;
            
            if(newState == PlayerState.UsingItem)
                onItemUsed.Raise(null);
                
        }

        public override void OnItemEquipped(Item_SO itemData)
        {
            base.OnItemEquipped(itemData);
            ItemData = itemData;
            itemRenderer.sprite = itemData.itemIcon;
        }
    }
}