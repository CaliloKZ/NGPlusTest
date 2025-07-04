using Inventory;
using UnityEngine;

namespace GameEvents.TypedEvents
{
    [CreateAssetMenu(menuName = "Scriptable Objects/GameEvent/InventorySlotEvent")]
    public class InventorySlotEvent : GameEvent<InventorySlot> {}
}