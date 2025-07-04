using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class ItemDescriptionUI : MonoBehaviour
    {
        [SerializeField] Image itemIconImage;
    
        [SerializeField] TextMeshProUGUI itemNameText;
        [SerializeField] TextMeshProUGUI itemDescriptionText;

        void OnEnable()
        {
            ResetItemDescription();
        }

        public void SetItemDescription(Sprite itemIcon, string itemName, string itemDescription)
        {
            if (itemIconImage != null)
            {
                itemIconImage.sprite = itemIcon;
                itemIconImage.enabled = true;
            }

            if (itemNameText != null)
                itemNameText.text = itemName;

            if (itemDescriptionText != null)
                itemDescriptionText.text = itemDescription;
        }

        void ResetItemDescription()
        {
            itemIconImage.sprite = null;
            itemIconImage.enabled = false;
            itemNameText.text = string.Empty;
            itemDescriptionText.text = string.Empty;
        }
    }
}
