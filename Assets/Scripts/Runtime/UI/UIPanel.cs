using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(Canvas), typeof(CanvasGroup))]
    public abstract class UIPanel : MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private string specifer = "base";
        private bool isActive;
        public event System.Action<bool> OnChangeActivity;
        public event System.Action OnOpen;
        public event System.Action OnClose;
        public bool IsActive => isActive;
        public string Specifer => specifer;
        public abstract void Initialize(UIController controller);
        public virtual void SetActivity(bool active)
        {
            isActive = active;
            enabled = active;
            group.interactable = active;
            canvas.enabled = active;
            OnChangeActivity?.Invoke(active);
        }
        public virtual void Open()
        {
            SetActivity(true);
            OnOpen?.Invoke();
        }
        public virtual void Close()
        {
            SetActivity(false);
            OnClose?.Invoke();
        }
    }
}