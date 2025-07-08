using GameEvents;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public abstract class SlotUI : MonoBehaviour
    {
        public InventorySlot Slot{get; private set;}
        
        [SerializeField] protected GameEvent<InventorySlot> onSlotSelected;
        [SerializeField] ItemAmountTextUI itemAmountTextUI;
        
        [SerializeField] Image itemIconImage;
        [SerializeField] Image itemSelectedBorder;
        
        public void Setup(InventorySlot slot)
        {
            Slot = slot;
            slot.OnSlotChanged += UpdateSlot;
        }

        private void OnDestroy()
        {
            Slot.OnSlotChanged -= UpdateSlot;
            Slot = null;
        }

        void UpdateSlot()
        {
            if (Slot.TryGetItemData(out Item_SO itemData))
            {
                itemIconImage.sprite = itemData.itemIcon;
                itemIconImage.enabled = true;
                itemAmountTextUI.SetItemAmount(Slot.StackSize, !Slot.CanStack(itemData.itemID));
                ToggleItemSelectedBorder(Slot.IsSelected);
                return;
            }

            itemIconImage.enabled = false;
            itemAmountTextUI.SetItemAmount(0);
            ToggleItemSelectedBorder(false);
        }
        
        void ToggleItemSelectedBorder(bool isSelected)
        {
            if (itemSelectedBorder != null)
                itemSelectedBorder.enabled = isSelected;
        }
    }
}