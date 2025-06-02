using System;
using UnityEngine;

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
