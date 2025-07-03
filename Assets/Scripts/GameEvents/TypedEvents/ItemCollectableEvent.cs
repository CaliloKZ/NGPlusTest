using Items;
using UnityEngine;

namespace GameEvents.TypedEvents
{
    [CreateAssetMenu(menuName = "Scriptable Objects/GameEvent/ItemCollectableEvent")]
    public class ItemCollectableEvent : GameEvent<ItemCollectable> {}
}