using UnityEngine;

namespace GameEvents
{
    public interface IGameEventListener<T> where T : MonoBehaviour
    {
        void OnEventRaised(T source);
    }
}