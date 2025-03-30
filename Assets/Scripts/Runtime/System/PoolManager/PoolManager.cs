using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace Management.Pool
{
    public interface IPoolObject
    {
        string Specifier { get; }
        string PoolTag { get; }
        bool InPool { get; set; }
        Transform Transform { get; }
        GameObject GameObject { get; }
        void OnPop();
        void OnPush();
    }
    public class PoolManager
    {
        protected class Pool
        {
            protected readonly Queue<IPoolObject> objects;
            protected GameObject prefab;
            protected Transform root;
            public Pool(GameObject prefab, Transform root)
            {
                this.prefab = prefab;
                this.root = root;
                objects = new Queue<IPoolObject>();
            }
            public bool Push(IPoolObject poolObject)
            {
                if (!poolObject.InPool)
                {
                    AddPoolObject(poolObject, Vector3.zero, Quaternion.identity);
                    return true;
                }
                else
                    return false;
            }
            public IPoolObject Pop(Vector3 position, Quaternion rotation)
            {
                if (objects.Count > 0)
                    return GetPoolObject(position, rotation);
                else
                    return CreatePoolObject(position, rotation);
            }
            private void AddPoolObject(IPoolObject poolObject, Vector3 position, Quaternion rotation)
            {
                poolObject.InPool = true;
                poolObject.GameObject.SetActive(false);
                Transform objectTran = poolObject.Transform;
                objectTran.SetParent(root);
                objectTran.SetPositionAndRotation(position, rotation);
                poolObject.OnPush();
                objects.Enqueue(poolObject);
            }
            private IPoolObject GetPoolObject(Vector3 position, Quaternion rotation)
            {
                IPoolObject poolObject = objects.Dequeue();
                poolObject.InPool = false;
                poolObject.Transform.SetPositionAndRotation(position, rotation);
                poolObject.GameObject.SetActive(true);
                poolObject.OnPop();
                return poolObject;
            }
            private IPoolObject CreatePoolObject(Vector3 position, Quaternion rotation)
            {
                GameObject go = GameObject.Instantiate(prefab, position, rotation, root);
                go.name = go.name + go.GetInstanceID().ToString();
                IPoolObject poolObject = go.GetComponent<IPoolObject>();
                poolObject.InPool = false;
                poolObject.OnPop();
                poolObject.GameObject.SetActive(true);
                return poolObject;
            }
        }

        private Dictionary<string, Pool> pools = new Dictionary<string, Pool>();
        private Transform root;

        public PoolManager(Transform root)
        {
            this.root = root;
        }
        public bool AddPool(IPoolObject prefab)
        {
            bool hasPrefab = prefab != null;
            if (hasPrefab)
            {
                string tag = prefab.PoolTag;
                if (!string.IsNullOrEmpty(tag) && !pools.ContainsKey(tag))
                {
                    GameObject prefabGO = prefab.GameObject;
                    Transform root = new GameObject(prefab.PoolTag).transform;
                    root.parent = this.root;
                    Pool pool = new Pool(prefabGO, root);
                    pools.Add(prefab.PoolTag, pool);
                    return true;
                }
            }
            return false;
        }
        public void Push(IPoolObject poolObject, bool addPoolOnFailure = true)
        {
            bool hasObject = poolObject != null;
            if (hasObject)
            {
                string tag = poolObject.PoolTag;
                Pool pool;
                if (pools.TryGetValue(tag, out pool))
                {
                    pool.Push(poolObject);
                }
                else
                {
                    if (addPoolOnFailure && AddPool(poolObject))
                    {
                        Push(poolObject, addPoolOnFailure);
                    }
                    else
                    {
                        poolObject.GameObject.SetActive(false);
                        poolObject.OnPush();
                    }
                }
            }
        }
        public IPoolObject Pop(IPoolObject poolObject, Vector3 position, Quaternion rotation, bool addPoolOnFailure = true)
        {
            var tag = poolObject.PoolTag;
            Pool pool;
            if (pools.TryGetValue(tag, out pool))
            {
                return pool.Pop(position, rotation);
            }
            else
            {
                if (addPoolOnFailure && AddPool(poolObject))
                {
                    return Pop(poolObject, position, rotation, addPoolOnFailure);
                }
                return null;
            }
        }
        public IPoolObject Pop(IPoolObject poolObject, Transform parent, bool addPoolOnFailure = true)
        {
            var result = Pop(poolObject, parent.position, parent.rotation, addPoolOnFailure);
            if (result != null)
                result.Transform.SetParent(parent, true);
            return result;
        }
    }
}