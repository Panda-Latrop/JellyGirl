using UnityEngine;

public class UIElement : MonoBehaviour
{
    [SerializeField] protected Canvas canvas;
    [SerializeField] protected CanvasGroup group;
    [SerializeField] protected float duration = 0.25f;

    public event System.Action<bool> OnVisibilityChange;

    public bool Interactable { get => group.interactable; set => group.interactable = value; }
    public float Duration => duration;
    public virtual void Show()
    {

        if (!enabled)
        {
            enabled = true;
            canvas.enabled = group.interactable = group.blocksRaycasts = true;

                group.alpha = 1f;
            OnVisibilityChange?.Invoke(true);

        }
    }

    public virtual void Hide()
    {
        if (enabled)
        {
            enabled = false;
            group.interactable = group.blocksRaycasts = false;

            
                group.alpha = 0f;
                canvas.enabled = false;
            
            OnVisibilityChange?.Invoke(false);
        }
    }
}
