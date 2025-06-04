using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class HotBarSlot : MonoBehaviour
    {
        [SerializeField] Image itemIconImage;
        [SerializeField] Image itemSelectedBorder;

        Item_SO _itemData;

        public void SetItemData(Item_SO itemData)
        {
            _itemData = itemData;
        }

        public void UpdateItemIcon(Sprite itemIcon)
        {
            itemIconImage.sprite = itemIcon;
            itemIconImage.enabled = true;
        }

        public void ResetSlot()
        {
            _itemData = null;
            itemIconImage.enabled = false;
            itemIconImage.sprite = null;
        }

        public void EquipItem()
        {
            if(null == _itemData)
                return;
            
            InventorySystem.OnItemEquipAction?.Invoke(_itemData);
        }
    }
}
