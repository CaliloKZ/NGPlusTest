using Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] InputActionReference toggleInventoryAction;
        
        [SerializeField] GameObject inventoryHolder;
        [SerializeField] GameObject gameplayHolder;
        
        void Awake()
        {
            toggleInventoryAction.action.performed += ToggleInventoryUI;
            toggleInventoryAction.action.Enable();
        }

        void OnDestroy()
        {
            toggleInventoryAction.action.performed -= ToggleInventoryUI;
            toggleInventoryAction.action.Disable();
        }
        
        void ToggleInventoryUI(InputAction.CallbackContext obj)
        {
            ToggleInventoryUI(!inventoryHolder.activeSelf);
        }
        
        public void ToggleInventoryUI(bool isActive)
        {
            inventoryHolder.SetActive(isActive);
            gameplayHolder.SetActive(!isActive);
            PlayerInputController.ToggleInputActions(!isActive);
            
            // if(!isActive)
            //     SaveController.StartSaveGame();
        }

        public void OnDialogUIToggle(bool isActive)
        {
            gameplayHolder.SetActive(!isActive);
            PlayerInputController.ToggleInputActions(!isActive);
        }
    }
}
