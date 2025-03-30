using UnityEngine;

namespace UI
{
    public abstract class UITransaction : AnimationAction
    {
        [SerializeField] private Canvas canvas;
        private System.Action actionOnFinish;
        private void Awake()
        {
            canvas.enabled = true;
        }
        public virtual void Init()
        {
         
            canvas.enabled = true;
        }
        protected override void Execute()
        {
            canvas.enabled = true;
            base.Execute();
        }
        public void Hide()
        {
            canvas.enabled = false;
        }
        public void SetFinishAction(System.Action actionOnFinish)
        {
            this.actionOnFinish = actionOnFinish;
        }
        protected override void Finish()
        {
            if (!inReverse)
            {
                inAnimation = false;
                actionOnFinish?.Invoke();
                actionOnFinish = null;
                Reverse();
            }
            else
            {
                canvas.enabled = false;
                base.Finish();
            }
        }
    }
}