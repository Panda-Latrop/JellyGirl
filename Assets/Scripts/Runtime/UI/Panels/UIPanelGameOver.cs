using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class UIPanelGameOver : UIPanel
    {
        [SerializeField] private UIButton restartButton, toMenuButton;

        public override void Initialize(UIController controller)
        {
            restartButton.AddListener(OnRestartButton);
            toMenuButton.AddListener(OnExitButton);
        }
        private void OnRestartButton()
        {
            GameInstance.Instance.UI.StartTransaction(Restart);
        }
        private void Restart()
        {
            GameInstance.Instance.RestartScene();
        }
        private void OnExitButton()
        {
            GameInstance.Instance.UI.StartTransaction(StartMainMenu);
        }
        private void StartMainMenu()
        {
            GameInstance.Instance.ChangeScene("MainMenu");
        }
    }
}
