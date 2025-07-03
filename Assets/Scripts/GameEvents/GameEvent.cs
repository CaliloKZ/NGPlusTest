using System.Collections.Generic;
using UnityEngine;

namespace GameEvents
{
    [CreateAssetMenu(fileName = "New GameEvent", menuName = "Scriptable Objects/GameEvent")]
    public class GameEvent : ScriptableObject
    {
        readonly List<GameEventListener> _listeners = new();
        
        public void Raise()
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventRaised();
        }
        
        public void RegisterListener(GameEventListener listener) => _listeners.Add(listener);
        public void UnregisterListener(GameEventListener listener) => _listeners.Remove(listener);
    }
    
    public abstract class GameEvent<T> : ScriptableObject where T : MonoBehaviour
    {
        private readonly List<IGameEventListener<T>> _listeners = new();

        public void Raise(T source)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
                _listeners[i].OnEventRaised(source);
        }

        public void RegisterListener(IGameEventListener<T> listener)
        {
            if (!_listeners.Contains(listener))
                _listeners.Add(listener);
        }

        public void UnregisterListener(IGameEventListener<T> listener)
        {
            _listeners.Remove(listener);
        }
    }
}
