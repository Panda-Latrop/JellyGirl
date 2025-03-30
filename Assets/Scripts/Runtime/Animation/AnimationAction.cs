using UnityEngine;
public abstract class AnimationAction : MonoBehaviour
{
    protected bool inAnimation;
    [SerializeField] private float duration = 1f;
    [SerializeField] private Ease ease = Ease.Linear;
    protected float currentTime;
    protected bool inReverse;
    public System.Action OnFinish;

    public bool InAnimation => inAnimation;
    [ContextMenu("Play")]
    public virtual void Play()
    {
        inReverse = false;
        Execute();
    }
    public virtual void Reverse()
    {
        inReverse = true;
        Execute();
    }
    protected virtual void Execute()
    {
        inAnimation = true;
        enabled = true;
        currentTime = 0.0f;
        Evaluate();
    }
    public void Stop()
    {
        Finish();
    }
    protected virtual void Finish()
    {
        enabled = inAnimation = false;
        OnFinish?.Invoke();
    }
    private void Evaluate()
    {
        float percent;
        if (inReverse)
            percent = EaseHandler.Evaluate(ease, 1.0f - (currentTime / duration));
        else
            percent = EaseHandler.Evaluate(ease, currentTime / duration);
        Action(percent);
        if (currentTime >= duration)
            Finish();
        currentTime += Time.unscaledDeltaTime;

    }
    protected abstract void Action(float percent);

    private void LateUpdate()
    {
        if (inAnimation)
        {
            Evaluate();
        }
        else
            enabled = false;
    }
}