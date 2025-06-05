using Player;
using SaveSystem;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] InputActionReference toggleInventoryAction;
        
        [SerializeField] GameObject inventoryUI;
        [SerializeField] GameObject gameplayUI;
        
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
            ToggleInventoryUI(!inventoryUI.activeSelf);
        }
        
        public void ToggleInventoryUI(bool isActive)
        {
            inventoryUI.SetActive(isActive);
            gameplayUI.SetActive(!isActive);
            PlayerInputController.ToggleInputActions(!isActive);
            
            if(!isActive)
                SaveController.StartSaveGame();
        }

        public void OnDialogUIToggle(bool isActive)
        {
            gameplayUI.SetActive(!isActive);
            PlayerInputController.ToggleInputActions(!isActive);
        }
    }
}
