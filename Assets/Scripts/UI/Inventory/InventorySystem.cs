using System;
using System.Collections.Generic;
using GameEvents;
using UI.Inventory;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    public static ItemDragHandler DragItem { get; private set; }
    public static Action<InventorySlot> OnBeginItemDragAction;
    public static Action OnEndItemDragAction;
    public static Action<InventorySlot, int> OnItemDropAction;
    
    [SerializeField] List<InventorySlot> inventorySlots = new();
    [SerializeField] ItemDragHandler dragItem;
    [SerializeField] ItemDescriptionUI itemDescriptionUI;
    [SerializeField] Item_SO testItemData;
    [SerializeField] Item_SO testItemData2;
    
    Dictionary<int, InventorySlot> slotsInstanceDictionary;

    

    void Awake()
    {
        slotsInstanceDictionary = new Dictionary<int, InventorySlot>();
        foreach (var slot in inventorySlots)
        {
            slotsInstanceDictionary.Add(slot.gameObject.GetInstanceID(), slot);
        }
        DragItem = dragItem;
        Test();
    }

    void Start()
    {
        OnItemDropAction += OnItemDrop;
    }

    void OnDestroy()
    {
        OnItemDropAction -= OnItemDrop;
    }

    void Test()
    {
        inventorySlots[0].SetItemData(testItemData);
        inventorySlots[3].SetItemData(testItemData2);
    }

    void OnItemDrop(InventorySlot oldSlot, int newSlotID)
    {
        if (!slotsInstanceDictionary.TryGetValue(newSlotID, out var newSlot)) 
            return;

        Item_SO oldSlotItemData = oldSlot.ItemData;

        if (newSlot.TryGetItemData(out Item_SO itemData))
        {
            newSlot.SetItemData(oldSlotItemData);
            oldSlot.SetItemData(itemData);
            return;
        }
        
        newSlot.SetItemData(oldSlotItemData);
        oldSlot.ResetSlot();
    }
}
