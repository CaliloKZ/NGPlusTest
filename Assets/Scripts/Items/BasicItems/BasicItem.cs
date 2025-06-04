using Inventory;
using UnityEngine;

namespace Items.BasicItems
{
    public class BasicItem : EquipItems
    {
        [SerializeField] SpriteRenderer itemRenderer;
        public override void OnItemEquipped(Item_SO itemData)
        {
            base.OnItemEquipped(itemData);
            itemRenderer.sprite = itemData.itemIcon;
        }
    }
}