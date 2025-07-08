using UI;
using UnityEngine;

namespace Inventory.UI
{
    public class HotBarSlotUI : SlotUI
    {
        [SerializeField] HotkeyButton hotkeyButton;
        
        void Awake()
        {
            hotkeyButton.OnClick.AddListener(OnClick);
        }

        void OnDestroy()
        {
            hotkeyButton.OnClick.RemoveAllListeners();
        }
        
        void OnClick()
        {
            onSlotSelected.Raise(Slot);
        }
    }
}
