using System;
using UnityEngine;

namespace Pool
{
    [CreateAssetMenu(fileName = "TypedPoolObjects", menuName = "Scriptable Objects/TypedPoolObjects")]
    public class TypedPoolObjects : ScriptableObject
    {
        public TypedPoolItem[] poolItems;
    
        [Serializable]
        public struct TypedPoolItem
        {
            public MonoBehaviour prefab;
            public int amount;
        }
    }
}
