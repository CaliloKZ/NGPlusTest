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
            PlayerInputController.ChangePlayerState(PlayerState.UsingItem);
        }
        
        public override void OnPlayerStateChanged()
        {
            PlayerState newState = PlayerInputController.CurrentState;
            
            if(newState == PlayerState.UsingItem)
                InventorySystem.OnItemUseAction?.Invoke();
        }

        public override void OnItemEquipped(Item_SO itemData)
        {
            base.OnItemEquipped(itemData);
            ItemData = itemData;
            itemRenderer.sprite = itemData.itemIcon;
        }
    }
}