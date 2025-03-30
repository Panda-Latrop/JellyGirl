using UnityEngine;

namespace UI
{
    public class UIPanelGameProcess : UIPanel
    {
        [SerializeField] private UIGameCondition condition;
        [SerializeField] private UIDash dash;
        public override void Initialize(UIController controller)
        {
            condition.Init();
            dash.Init();
        }
    }
}
