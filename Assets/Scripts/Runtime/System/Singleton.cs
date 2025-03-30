using UnityEngine;

namespace Management.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;
        protected bool isOrigin;
        public static T Instance { get => instance;}
        public static bool HasInstance { get; private set; }
        private void Awake()
        {
            OnAwake();
        }
        protected virtual void OnAwake()
        {
            Initialization();
        }
        private void Initialization()
        {
            if (!HasInstance)
            {
                instance = (T)this;
                HasInstance = true;
                isOrigin = true;
                DontDestroyOnLoad(Instance.gameObject);
            }
            else
            {
                DestroyImmediate(this);
            }
        }

    }
}
