using System.Collections.Generic;
using GameEvents;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] List<HotBarSlotUI> gameplaySlots = new();
        [SerializeField] List<InventorySlotUI> slots = new();
        [SerializeField] ItemDescriptionUI itemDescription;
        [SerializeField] Image equippedItemImage;


        public void ClearSelectedItem()
        {
            equippedItemImage.enabled = false;
        }
        public void ItemSelected(Item_SO itemData)
        {
            equippedItemImage.sprite = itemData.itemIcon;
            equippedItemImage.enabled = true;
            itemDescription.SetItemDescription(itemData.itemIcon, itemData.itemName, itemData.itemDescription);
        }
        
        public void SlotSetup(InventorySlot slot)
        {
            if(slot.SlotIndex > slots.Count - 1)
                return;
            
            slots[slot.SlotIndex].Setup(slot);
            
            if(slot.SlotIndex > gameplaySlots.Count - 1)
                return;
            
            gameplaySlots[slot.SlotIndex].Setup(slot);
        }
    }
}