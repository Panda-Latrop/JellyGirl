using UnityEngine;
namespace Management.Pool
{
    public abstract class PoolObject : MonoBehaviour, IPoolObject
    {
        [SerializeField] protected string specifier = "Base";
        private bool inPool;
        public string Specifier => specifier;
        string IPoolObject.PoolTag => $"{GetType().Name} {specifier}";
        bool IPoolObject.InPool { get => inPool; set => inPool = value; }
        Transform IPoolObject.Transform => transform;
        GameObject IPoolObject.GameObject => gameObject;
        public abstract void OnPop();
        public abstract void OnPush();
    }
}
