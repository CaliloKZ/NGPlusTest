using Inventory;
using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Items
{
    public abstract class EquipItems : MonoBehaviour
    {
        [field: SerializeField] public Item_SO ItemData { get; protected set; }
        DefaultInputActions _playerInputActions;

        protected virtual void OnEnable()
        {
            _playerInputActions = PlayerInputController.InputActions;
            if (_playerInputActions == null) 
                return;
            
            _playerInputActions.Player.Fire.started += OnFireAction;
        }

        protected virtual void OnDisable()
        {
            if (_playerInputActions == null) 
                return;
            
            _playerInputActions.Player.Fire.started -= OnFireAction;
        }

        protected virtual void OnFireAction(InputAction.CallbackContext obj) { }
        
        public virtual void OnPlayerStateChanged(){}
        
        public virtual void OnItemEquipped(Item_SO itemData)
        {
            gameObject.SetActive(true);
        }
        
        public virtual void OnItemUnequipped()
        {
            gameObject.SetActive(false);
        }
    }
}