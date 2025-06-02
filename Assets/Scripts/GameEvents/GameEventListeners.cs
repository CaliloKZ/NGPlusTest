using System.Collections.Generic;
using UnityEngine;

namespace GameEvents
{
    public class GameEventListeners : MonoBehaviour
    {
        [SerializeField] List<GameEventListener> listeners = new();

        void OnEnable()
        {
            for (var i = 0; i < listeners.Count; i++)
            {
                listeners[i].OnEnable();
            }
        }

        void OnDisable()
        {
            for (var i = 0; i < listeners.Count; i++)
            {
                listeners[i].OnDisable();
            }
        }
    }
}
