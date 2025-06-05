using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pool
{
    public class FastPool : MonoBehaviour
    {
        [SerializeField] PoolObjects poolObjects;
        [SerializeField] TypedPoolObjects typedPoolObjects;
        static FastPool _instance;
        
        Dictionary<string, Queue<GameObject>> _pool;
        Dictionary<string, Queue<MonoBehaviour>> _typedPool;

        void Awake()
        {
            if (null != _instance)
                Destroy(_instance);

            _instance = this;
            _pool = new Dictionary<string, Queue<GameObject>>();
            _typedPool = new Dictionary<string, Queue<MonoBehaviour>>();

             foreach (var item in poolObjects.poolItems)
             {
                 Queue<GameObject> pool = new Queue<GameObject>();
                 for (int i = 0; i < item.amount; i++)
                 {
                     GameObject prefab = Instantiate(item.prefab, transform, true);
                     prefab.name = item.prefab.name;
                     prefab.SetActive(false);
                     pool.Enqueue(prefab);
                 }
                 _pool.Add(item.prefab.name, pool);
             }
             
             foreach (var item in typedPoolObjects.poolItems)
             {
                 Queue<MonoBehaviour> pool = new Queue<MonoBehaviour>();
                 for (int i = 0; i < item.amount; i++)
                 {
                     MonoBehaviour prefab = Instantiate(item.prefab, transform, true);
                     prefab.name = item.prefab.name;
                     prefab.gameObject.SetActive(false);
                     pool.Enqueue(prefab);
                 }
                 _typedPool.Add(item.prefab.name, pool);
             }
        }

        public static T Instantiate<T>(string id, Vector3 position = default, Quaternion rotation = default,
            Transform parent = null) where T : MonoBehaviour
        {
            if (!_instance._typedPool.TryGetValue(id, out Queue<MonoBehaviour> pool))
            {
                Debug.LogError("Pool does not contain object with id: " + id);
                return default;
            }

            if (pool.Count <= 1)
            {
                Debug.LogWarning($"New object of type {id} is being instantiated");
                
                MonoBehaviour prefab = Instantiate(pool.Peek(), _instance.transform, true);
                prefab.name = pool.Peek().name;
                prefab.gameObject.SetActive(false);
                pool.Enqueue(prefab);
            }
            
            MonoBehaviour poolTypedObject = pool.Peek();
            Transform transform = poolTypedObject.transform;

            transform.SetParent(parent);
            transform.position = position;
            transform.rotation = rotation;
            poolTypedObject.gameObject.SetActive(true);

            MonoBehaviour spawn = pool.Dequeue();
            return spawn as T;
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
                prefab.name = pool.Peek().name;
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
        
        public static void Destroy<T>(T typedObject) where T : MonoBehaviour
        {
            if (null == typedObject)
                return;

            if (!_instance._typedPool.TryGetValue(typedObject.gameObject.name, out Queue<MonoBehaviour> pool))
            {
                Debug.LogError("Pool does not contain object with name: " + typedObject.name);
                return;
            }

            typedObject.gameObject.SetActive(false);
            pool.Enqueue(typedObject);
        }
    }
    
}

