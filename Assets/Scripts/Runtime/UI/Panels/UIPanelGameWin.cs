using UnityEngine;

namespace UI
{
    public class UIPanelGameWin : UIPanel
    {
        [SerializeField] private string nextLevel = "Level";
        [SerializeField] private UIButton startButton, exitButton;

        public override void Initialize(UIController controller)
        {

            startButton.AddListener(OnNxetClick);
            exitButton.AddListener(OnExitButton);
        }
        private void OnExitButton()
        {
            GameInstance.Instance.UI.StartTransaction(StartMainMenu);

        }

        private void StartMainMenu()
        {
            GameInstance.Instance.ChangeScene("MainMenu");
        }

        private void OnNxetClick()
        {
            GameInstance.Instance.UI.StartTransaction(StartNxet);
        }
        private void StartNxet()
        {

            GameInstance.Instance.ChangeScene(nextLevel);
        }
    }
}
