using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BattleArena.Pool
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private List<PoolInfo> _objectsToPool;

        private List<PoolObject> _pooledObjects;
        private List<Type> _pooledTypes;

        public void Initialize()
        {
            _pooledObjects = new List<PoolObject>();
            _pooledTypes = _objectsToPool.Select(o => o.PoolObject.GetType()).Distinct().ToList();
            foreach (var objectToPool in _objectsToPool)
            {
                for (var i = 0; i < objectToPool.Amount; i++)
                {
                    var tmp = Instantiate(objectToPool.PoolObject, transform);
                    tmp.gameObject.SetActive(false);
                    _pooledObjects.Add(tmp);
                }
            }
        }

        public T GetObject<T>() where T : PoolObject
        {
            if (_pooledTypes.All(o => o != typeof(T)))
            {
                throw new ArgumentException($"Invalid type of object {typeof(T)}");
            }

            var pooledObject = _pooledObjects.Find(o => o is T);
            if (pooledObject != null)
            {
                _pooledObjects.Remove(pooledObject);
                pooledObject.gameObject.SetActive(true);
                pooledObject.transform.parent = null;
                return pooledObject as T;
            }

            var newObject = Instantiate(_objectsToPool.Find(o => o.PoolObject is T).PoolObject);
            return newObject as T;
        }

        public void ReturnToPool<T>(T poolObject) where T : PoolObject
        {
            if (_pooledTypes.All(o => o != typeof(T)))
            {
                throw new ArgumentException($"Invalid type of object {typeof(T)}");
            }

            poolObject.gameObject.SetActive(false);
            poolObject.transform.SetParent(transform);
            _pooledObjects.Add(poolObject);
        }
    }
}