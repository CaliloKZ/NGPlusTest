using System;
using Pool;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{ public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
    { 
        [field: SerializeField] public InventorySlot_SO SlotData { get; private set; }

        [SerializeField] ItemAmountTextUI itemAmountTextUI;
        
        [SerializeField] Image itemIconImage;
        [SerializeField] Image itemSelectedBorder;

        ItemDragHandler _dragHandler;

        void OnEnable()
        {
            UpdateSlot();
        }

        public void SetItemData(Item_SO newItemData, int itemAmount)
        {
            SlotData.SetItem(newItemData, itemAmount);
            UpdateSlot();
        }

        public void ClearSlot()
        {
            SlotData.ClearSlot();
            UpdateSlot();
        }

        void UpdateSlot()
        {
            bool hasItem = SlotData.TryGetItemData(out var itemData);
            
            itemAmountTextUI.SetItemAmount(SlotData.stackSize, hasItem && itemData.maxStackSize == SlotData.stackSize);
            itemIconImage.sprite = hasItem ? itemData.itemIcon : null;
            itemIconImage.enabled = hasItem;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(!SlotData.TryGetItemData(out _))
                return;
        
            if(null == _dragHandler)
                _dragHandler = InventorySystem.DragItem;
        
            InventorySystem.OnBeginItemDragAction?.Invoke(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(null == _dragHandler)
                return;
        
            _dragHandler.transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            InventorySystem.OnEndItemDragAction?.Invoke();
            if (null == eventData.pointerEnter)
                return;
            
            InventorySystem.OnItemDropAction?.Invoke(this, eventData.pointerEnter.GetInstanceID());
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if(!SlotData.TryGetItemData(out _))
                return;
            
            InventorySystem.OnItemSelectAction?.Invoke(this);
        }
        
        public void ToggleItemSelectedBorder(bool isSelected)
        {
            if (itemSelectedBorder != null)
                itemSelectedBorder.enabled = isSelected;
        }

        public void OnItemUsed()
        {
            SlotData.ChangeItemAmount(-1);

            if (SlotData.stackSize <= 0)
            {
                ClearSlot();
                InventorySystem.OnItemUnequipAction?.Invoke();
            }
            
            InventorySystem.OnSlotUpdateAction?.Invoke(SlotData);
        }
    }
}
