using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class HotBarSlot : MonoBehaviour
    {
        [field: SerializeField] public InventorySlot_SO SlotData { get; private set; }
        
        [SerializeField] ItemAmountTextUI itemAmountTextUI;
        
        [SerializeField] Image itemIconImage;
        [SerializeField] Image itemSelectedBorder;

        void Awake()
        {
            InventorySystem.OnSlotUpdateAction += UpdateSlot;
        }

        void OnDestroy()
        {
            InventorySystem.OnSlotUpdateAction -= UpdateSlot;
        }

        void OnEnable()
        {
            if(null == SlotData.itemData)
                return;
            
            UpdateSlot(SlotData);
        }

        void OnDisable()
        {
            ClearSlot();
        }

        void UpdateSlot(InventorySlot_SO slotData)
        {
            bool hasItem = SlotData.TryGetItemData(out var itemData);
            
            itemAmountTextUI.SetItemAmount(SlotData.stackSize, hasItem && itemData.maxStackSize == SlotData.stackSize);
            itemIconImage.sprite = hasItem ? itemData.itemIcon : null;
            itemIconImage.enabled = hasItem;
            itemSelectedBorder.enabled = InventorySystem.EquippedItemSlot == SlotData;
        }

        public void ClearSlot()
        {
            itemIconImage.enabled = false;
            itemIconImage.sprite = null;
            itemSelectedBorder.enabled = false;
            itemAmountTextUI.SetItemAmount(0);
        }

        public void EquipItem()
        {
            if(null == SlotData.itemData)
                return;
            
            InventorySystem.OnItemEquipAction?.Invoke(SlotData);
        }
    }
}
