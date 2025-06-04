using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemDragHandler : MonoBehaviour
    {
        [SerializeField] Image itemIconImage;

        void Start()
        {
            InventorySystem.OnBeginItemDragAction += OnBeginDrag;
            InventorySystem.OnEndItemDragAction += OnEndDrag;
        }

        private void OnDestroy()
        {
            InventorySystem.OnBeginItemDragAction -= OnBeginDrag;
            InventorySystem.OnEndItemDragAction -= OnEndDrag;
        }

        void OnBeginDrag(InventorySlot slot)
        {
            transform.position = slot.transform.position;
        
            itemIconImage.sprite = slot.ItemData.itemIcon;
            itemIconImage.enabled = true;
        }

        void OnEndDrag()
        {
            itemIconImage.enabled = false;
            itemIconImage.sprite = null;
        }
    }
}
