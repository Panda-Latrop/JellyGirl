using UnityEngine;

namespace UI
{
    [RequireComponent(typeof(UIPanel))]
    public class UIAnimationPanelVisibility : AnimationAction
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private CanvasGroup group;
        [SerializeField] private UIPanel panel;

        private void Awake()
        {
            panel.OnOpen += Play;
            panel.OnClose += Reverse;
        }
        private void OnDestroy()
        {
            panel.OnOpen -= Play;
            panel.OnClose -= Reverse;
        }
        protected override void Execute()
        {
            canvas.enabled = true;
            base.Execute();
        }
        protected override void Action(float percent)
        {
            group.alpha = Mathf.Lerp(0.0f, 1.0f, percent);
        }
        protected override void Finish()
        {
            if (inReverse)
                group.alpha = 0.0f;
            else
                group.alpha = 1.0f;
            canvas.enabled = !inReverse;
            base.Finish();
        }
    }
}
