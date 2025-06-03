using UnityEngine;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class HotBarSlot : MonoBehaviour
    {
        [SerializeField] Image itemIconImage;
        [SerializeField] Image itemSelectedBorder;

        InventorySlot _slot;

        public void SetSlot(InventorySlot slot)
        {
            _slot = slot;
        }

        public void EquipItem()
        {
            
        }
    }
}
