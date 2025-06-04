using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Inventory
{ public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
    { 
        public Item_SO ItemData { get; private set; }

        [SerializeField] Image itemIconImage;
        [SerializeField] Image itemSelectedBorder;
        [SerializeField] HotBarSlot hotBarSlot;

        ItemDragHandler _dragHandler;

        void Start()
        {
            if(null == hotBarSlot)
                return;
            
            hotBarSlot.SetSlot(this);
        }

        public bool TryGetItemData(out Item_SO itemData)
        {
            itemData = ItemData;
            return itemData != null;
        }

        public void SetItemData(Item_SO newItemData)
        {
            ItemData = newItemData;
            UpdateItemIcon();
        }

        public void ResetSlot()
        {
            ItemData = null;
            if (itemIconImage == null)
                return;
        
            itemIconImage.sprite = null;
            itemIconImage.enabled = false;
        }

        void UpdateItemIcon()
        {
            if (null == itemIconImage || null == ItemData)
                return;
            
            itemIconImage.sprite = ItemData.itemIcon;
            itemIconImage.enabled = true;
            
            
            if (null == hotBarSlot)
                return;

            hotBarSlot.UpdateItemIcon(ItemData.itemIcon);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if(null == ItemData)
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
            if (null == ItemData)
                return;
            
            InventorySystem.OnItemSelectAction?.Invoke(this);
        }
        
        public void ToggleItemSelectedBorder(bool isSelected)
        {
            if (itemSelectedBorder != null)
                itemSelectedBorder.enabled = isSelected;
        }
    }
}
