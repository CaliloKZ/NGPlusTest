using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class FastPool : MonoBehaviour
    {
        [SerializeField] PoolObjects poolObjects;
        static FastPool _instance;
        
        Dictionary<string, Queue<GameObject>> _pool;

        void Awake()
        {
            if (null != _instance)
                Destroy(_instance);

            _instance = this;
            _pool = new Dictionary<string, Queue<GameObject>>();

             foreach (var item in poolObjects.poolItems)
             {
                 Queue<GameObject> pool = new Queue<GameObject>();
                 for (int i = 0; i < item.amount; i++)
                 {
                     GameObject prefab = Instantiate(item.prefab, transform, true);
                     prefab.SetActive(false);
                     pool.Enqueue(prefab);
                 }
                 _pool.Add(item.prefab.name, pool);
             }
        }

        public static GameObject Instantiate(string id, Vector3 position = default, Quaternion rotation = default,
            Transform parent = null)
        {
            if (!_instance._pool.TryGetValue(id, out Queue<GameObject> pool))
            {
                Debug.LogError("Pool does not contain object with id: " + id);
                return null;
            }

            if (pool.Count <= 1)
            {
                Debug.LogWarning($"New object of type {id} is being instantiated");
                
                GameObject prefab = Instantiate(pool.Peek(), _instance.transform, true);
                prefab.SetActive(false);
                pool.Enqueue(prefab);
            }
            
            GameObject poolGameObject = pool.Peek();
            Transform transform = poolGameObject.transform;

            transform.SetParent(parent);
            transform.position = position;
            transform.rotation = rotation;
            poolGameObject.SetActive(true);

            GameObject spawn = pool.Dequeue();
            return spawn;
        }
        
        public static void Destroy(GameObject gameObject)
        {
            if (null == gameObject)
                return;

            if (!_instance._pool.TryGetValue(gameObject.name, out Queue<GameObject> pool))
            {
                Debug.LogError("Pool does not contain object with name: " + gameObject.name);
                return;
            }

            gameObject.SetActive(false);
            pool.Enqueue(gameObject);
        }
    }
}

