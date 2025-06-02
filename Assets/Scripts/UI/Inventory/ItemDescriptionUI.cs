using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionUI : MonoBehaviour
{
    [SerializeField] Image itemIconImage;
    
    [SerializeField] TextMeshProUGUI itemNameText;
    [SerializeField] TextMeshProUGUI itemDescriptionText;
    
    public void SetItemDescription(Sprite itemIcon, string itemName, string itemDescription)
    {
        ResetItemDescription();
        if (itemIconImage != null)
            itemIconImage.sprite = itemIcon;

        if (itemNameText != null)
            itemNameText.text = itemName;

        if (itemDescriptionText != null)
            itemDescriptionText.text = itemDescription;
    }

    void ResetItemDescription()
    {
        itemIconImage.sprite = null;
        itemNameText.text = string.Empty;
        itemDescriptionText.text = string.Empty;
    }
    
}
