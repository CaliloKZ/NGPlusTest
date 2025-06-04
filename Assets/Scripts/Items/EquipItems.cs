using Inventory;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Items
{
    public abstract class EquipItems : MonoBehaviour
    {
        [field: SerializeField] public Item_SO ItemData { get; private set; }
        InputActionReference _fireActionReference;
        
        public void SetFireActionReference(InputActionReference actionReference)
        {
            _fireActionReference = actionReference;
        }

        protected virtual void OnEnable()
        {
            if (_fireActionReference == null) 
                return;
            
            _fireActionReference.action.performed += OnFireAction;
        }

        protected virtual void OnDisable()
        {
            if (_fireActionReference == null) 
                return;
            
            _fireActionReference.action.performed -= OnFireAction;
        }

        protected virtual void OnFireAction(InputAction.CallbackContext obj) { }
        
        public virtual void OnPlayerStateChanged(){}

        protected virtual void ToggleFireAction(bool isActive)
        {
            if (isActive)
            {
                _fireActionReference.action.Enable();
                return;
            }
            
            _fireActionReference.action.Disable();
        }
        
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