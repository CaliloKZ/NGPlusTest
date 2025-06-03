using UnityEngine;

namespace UI
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] GameObject inventoryUI;
        [SerializeField] GameObject gameplayUI;
    
        public void ToggleInventoryUI(bool isActive)
        {
            inventoryUI.SetActive(isActive);
            gameplayUI.SetActive(!isActive);
        }
    }
}
