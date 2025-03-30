using UnityEngine;

namespace UI
{
    public class UIAnimationButtonClick : AnimationAction
    {
        [SerializeField] private UIButton button;
        private Transform target;
        [SerializeField] private float scale = 0.5f;

        private void Awake()
        {
            button.AddListener(Play);
            target = button.transform;
        }
        private void OnDestroy()
        {
            button.RemoveListener(Play);
        }
        private void OnDisable()
        {
            if (inAnimation)
                Finish();
        }
        protected override void Finish()
        {
            base.Finish();
            target.localScale = Vector3.one;
        }
        protected override void Action(float percent)
        {
            target.localScale = Vector3.LerpUnclamped(Vector3.one * scale, Vector3.one, percent);
        }
    }
}