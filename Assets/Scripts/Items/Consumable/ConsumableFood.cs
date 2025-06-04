using Inventory;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Items.Consumable
{
    public class ConsumableFood : EquipItems
    {
        [SerializeField] SpriteRenderer itemRenderer;
        protected override void OnFireAction(InputAction.CallbackContext obj)
        {
            PlayerInputController.ChangePlayerState(PlayerInputController.PlayerState.UsingItem);
        }
        
        public override void OnPlayerStateChanged()
        {
            PlayerInputController.PlayerState newState = PlayerInputController.CurrentState;
            bool isUsingItem = newState == PlayerInputController.PlayerState.UsingItem;
            ToggleFireAction(isUsingItem);
            
            if(isUsingItem)
                UseItem();
        }

        public override void OnItemEquipped(Item_SO itemData)
        {
            base.OnItemEquipped(itemData);
            itemRenderer.sprite = itemData.itemIcon;
        }

        void UseItem()
        {
            OnItemUnequipped();
        }
    }
}