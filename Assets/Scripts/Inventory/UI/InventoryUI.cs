using System.Collections.Generic;
using UnityEngine;

namespace Inventory.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] List<InventorySlotUI> slots = new();

        public void UpdateSlot(InventorySlot slot)
        {
            slots[slot.SlotIndex].UpdateSlot(slot);
        }
    }
}