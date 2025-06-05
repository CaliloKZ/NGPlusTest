using System;
using UnityEngine;

namespace Pool
{
    [CreateAssetMenu(fileName = "PoolObjects", menuName = "Scriptable Objects/PoolObjects")]
    public class PoolObjects : ScriptableObject
    {
        public PoolItem[] poolItems;
    
        [Serializable]
        public struct PoolItem
        {
            public GameObject prefab;
            public int amount;
        }
    }
}
