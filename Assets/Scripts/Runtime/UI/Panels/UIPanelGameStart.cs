using UnityEngine;

namespace UI
{
    public class UIPanelGameStart : UIPanel
    {
        [SerializeField] private UIButton startButton;

        public override void Initialize(UIController controller)
        {
            startButton.AddListener(OnStartButtonClick);
        }
        private void OnStartButtonClick()
        {

        }
    }
}