using System.Collections.Generic;
using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(fileName = "New GameEvent", menuName = "Scriptable Objects/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        List<GameEventListener> _listeners = new();
        
        public void Raise()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventRaised();
        }
        
        public void RegisterListener(GameEventListener listener) => _listeners.Add(listener);
        public void UnregisterListener(GameEventListener listener) => _listeners.Remove(listener);
    }
}
