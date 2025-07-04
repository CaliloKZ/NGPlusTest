using System;
using Pool;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.UI;

namespace Inventory
{ public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
    { 
        [SerializeField] ItemAmountTextUI itemAmountTextUI;
        
        [SerializeField] Image itemIconImage;
        [SerializeField] Image itemSelectedBorder;
        
        ItemDragHandler _dragHandler;

        public void UpdateSlot(InventorySlot slot)
        {
            if (slot.TryGetItemData(out Item_SO itemData))
            {
                itemIconImage.sprite = itemData.itemIcon;
                itemIconImage.gameObject.SetActive(true);
                itemAmountTextUI.SetItemAmount(slot.StackSize, !slot.CanStack(itemData.itemID));
                ToggleItemSelectedBorder(slot.IsSelected);
                return;
            }
            
            itemIconImage.gameObject.SetActive(false);
            itemAmountTextUI.SetItemAmount(0);
            ToggleItemSelectedBorder(false);
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if(null == _dragHandler)
                return;
        
            _dragHandler.transform.position = eventData.position;
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
        }
        
        public void ToggleItemSelectedBorder(bool isSelected)
        {
            if (itemSelectedBorder != null)
                itemSelectedBorder.enabled = isSelected;
        }
        
        public void OnItemUsed()
        {

        }
    }
}
