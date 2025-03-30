using UnityEngine;

namespace UI
{
    public class UITransactionRectRise : UITransaction
    {
        [SerializeField] private RectTransform imageRect;
        [SerializeField] private float minSize = 0.1f;
        [SerializeField] private float maxSize = 10.0f;
        public override void Init()
        {
            imageRect.localScale = Vector3.one * maxSize;
        }
        protected override void Action(float percent)
        {
            imageRect.localScale = Vector3.Lerp(Vector3.one * maxSize, Vector3.one * minSize, percent);
        }
    }
}
