using Inventory;
using UnityEngine;

namespace GameEvents.TypedEvents
{
    [CreateAssetMenu(menuName = "Scriptable Objects/GameEvent/InventorySlotsEvent")]
    public class InventorySlotGameObjectEvent : GameEvent<InventorySlot, GameObject> {}
}