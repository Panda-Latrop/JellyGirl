using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class UIPanelPause : UIPanel
{
    [SerializeField] private UIButton resumeButton;
    [SerializeField] private UIButton restartButton;
    [SerializeField] private UIButton exitButton;
    public override void Initialize(UIController controller)
    {
        resumeButton.AddListener(OnResumeClick);
        restartButton.AddListener(OnRestartButton);
        exitButton.AddListener(OnExitButton);
    }

    private void OnResumeClick()
    {
        //GameInstance.Instance.UI.Change("process");
        GameInstance.Instance.GameState.Resume();
    }
    private void OnRestartButton()
    {
        GameInstance.Instance.UI.StartTransaction(GameInstance.Instance.RestartScene);

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
