using UnityEngine;

namespace UI
{
    public class UIButtonChangePanel : UIButton
    {

        [SerializeField] private UIPanel panel;
        public override void Click()
        {
           GameInstance.Instance.UI.Change(panel);
        }
    }
}
