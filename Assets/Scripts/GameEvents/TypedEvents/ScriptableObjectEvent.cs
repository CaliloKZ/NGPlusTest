using UnityEngine;

namespace GameEvents.TypedEvents
{
    [CreateAssetMenu(menuName = "Scriptable Objects/GameEvent/ScriptableObjectEvent")]
    public class ScriptableObjectEvent : GameEvent<ScriptableObject>{}
}