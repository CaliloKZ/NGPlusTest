using TMPro;
using UnityEngine;

namespace Inventory
{
    public class ItemAmountTextUI : MonoBehaviour
    {
        [SerializeField] TMP_Text itemAmountText;
    
        [SerializeField] Color basicColor;
        [SerializeField] Color fullColor;
    
        public void SetItemAmount(int amount, bool isFull = false)
        {
            if(amount <= 1)
            {
                itemAmountText.SetText(string.Empty);
                return;
            }
            
            itemAmountText.SetText(amount.ToString());
            itemAmountText.color = isFull ? fullColor : basicColor;
        }

    }
}
