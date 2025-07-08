using GameEvents;
using Pool;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory.UI
{ public class InventorySlotUI : SlotUI, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler
    {
        const string ITEM_DRAG_HANDLER_ID = "ItemDragHandler";
        [SerializeField] GameEvent<InventorySlot, GameObject> onEndItemDrag;
        
        ItemDragHandler _dragHandler;
        public void OnBeginDrag(PointerEventData eventData)
        {
            if(!Slot.TryGetItemData(out _))
                return;

            if (null == _dragHandler)
                _dragHandler = FastPool.Instantiate<ItemDragHandler>(ITEM_DRAG_HANDLER_ID, eventData.position, Quaternion.identity, transform.parent.parent);
            
            _dragHandler.UpdateItemIcon(Slot.ItemData.itemIcon);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(null == _dragHandler)
                return;
        
            _dragHandler.transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (null == eventData.pointerEnter || 
                null == _dragHandler)
                return;
            
            FastPool.Destroy(_dragHandler);
            _dragHandler = null;
            onEndItemDrag.Raise(Slot, eventData.pointerEnter);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if(!Slot.TryGetItemData(out _))
                return;
            
            onSlotSelected.Raise(Slot);
        }
    }
}
