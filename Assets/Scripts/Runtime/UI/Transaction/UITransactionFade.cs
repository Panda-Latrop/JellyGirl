using UnityEngine;

namespace UI
{
    using UnityEngine.UI;

    public class UITransactionFade : UITransaction
    {
        [SerializeField] private Graphic graphic;
        public override void Init()
        {
            Color color = graphic.color;
            color.a = 0f;
            graphic.color = color;
        }
        protected override void Action(float percent)
        {
            Color color = graphic.color;
            color.a = Mathf.Lerp(0f, 1f, percent);
            graphic.color = color;
        }
    }
}
