using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class ItemDragHandler : MonoBehaviour
    {
        [SerializeField] Image itenIconImage;

        public void UpdateItemIcon(Sprite icon)
        {
            itenIconImage.sprite = icon;
        }
    }
}