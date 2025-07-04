using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemDragHandler : MonoBehaviour
    {
        [SerializeField] Image itemIconImage;

        void OnBeginDrag(InventorySlotUI slotUI)
        {
            transform.position = slotUI.transform.position;
        
            //itemIconImage.sprite = slotUI.SlotData.itemData.itemIcon;
            itemIconImage.enabled = true;
        }

        void OnEndDrag()
        {
            itemIconImage.enabled = false;
            itemIconImage.sprite = null;
        }
    }
}
