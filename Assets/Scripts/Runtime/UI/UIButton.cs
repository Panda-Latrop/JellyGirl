using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Button))]
    public class UIButton : MonoBehaviour
    {
        [SerializeField] protected Button button;
        protected bool isLocked;
        public event System.Action<bool> OnInteractableChange;
        public virtual bool Interactable
        {
            get => button.interactable;
            set
            {
                if (button.interactable != value && !isLocked)
                {
                    button.interactable = value;
                    OnInteractableChange?.Invoke(value);
                }
            }
        }
        public bool IsLocked { get => isLocked; set { isLocked = value; } }
        private void Start()
        {
            button.onClick.AddListener(Click);
        }
        protected virtual void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }
        public void AddListener(UnityAction action)
        {
            button.onClick.AddListener(action);
        }
        public void RemoveListener(UnityAction action)
        {
            button.onClick.RemoveListener(action);
        }
        public virtual void Click(){}

    }
}