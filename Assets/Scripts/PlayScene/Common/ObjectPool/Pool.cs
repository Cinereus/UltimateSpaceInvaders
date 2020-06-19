using System.Collections.Generic;
using UnityEngine;

namespace PlayScene.Common.ObjectPool
{
    public class Pool : MonoBehaviour
    {
        public int Count => _poolObjects.Count;
        private List<GameObject> _poolObjects = new List<GameObject>();
        private static Dictionary<string, Pool> _createdPools = new Dictionary<string, Pool>();

        public static Pool CreatePool(GameObject poolObject, int size)
        {
            if (size < 0)
            {
                Debug.LogError("Size must be greater than 0!");
            }
            else if (poolObject.GetComponent<PoolObject>() == null)
            {
                Debug.LogError(poolObject.name+" gameObject doesn't have poolObject component");
            }
        
            var objectName = poolObject.gameObject.name;
        
            if (_createdPools.ContainsKey(objectName))
            {
                var pool = _createdPools[objectName];
            
                if (size > pool.Count)
                {
                    var sizeToExtend = size - pool.Count;
                    pool.AddObjectToPool(poolObject, sizeToExtend);    
                }
            
                return _createdPools[objectName];
            }
            else
            {
                var pool = new GameObject(objectName+"Pool").AddComponent<Pool>();;
            
                pool.AddObjectToPool(poolObject, size);
                _createdPools.Add(objectName, pool);
                return pool;
            }
        }

        public GameObject GetObject()
        {
            foreach (var poolObject in _poolObjects)
            {
                if (!poolObject.activeInHierarchy)
                {
                    poolObject.SetActive(true);
                    return poolObject;
                }
            }
        
            AddObjectToPool(_poolObjects[0], 10);
            return GetObject();
        }

        private void OnDestroy()
        {
            _createdPools.Clear();
        }

        private void AddObjectToPool(GameObject objectToAdd, int size)
        {
            for (var i = 0; i < size; i++)
            {
                var newObject = Instantiate(objectToAdd, transform);
                var poolObjectComponent = newObject.GetComponent<PoolObject>();
                poolObjectComponent.SaveParentReference(transform);
                newObject.SetActive(false);
                _poolObjects.Add(newObject);
            }
        }
    }
}
