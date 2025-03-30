using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class UIPanelMainMenu : UIPanel
{
    [SerializeField] private UIButton startButton,prologueButton,tutorialButton,exitButton;
    public override void Initialize(UIController controller)
    {
        startButton.AddListener(OnStarClick);
        tutorialButton.AddListener(OnTutorialClick);
        prologueButton.AddListener(OnPrologueClick);
        exitButton.AddListener(OnExitClick);
    }
    private void OnPrologueClick()
    {
        GameInstance.Instance.UI.StartTransaction(StartPrologue);
    }
    private void OnStarClick()
    {
        GameInstance.Instance.UI.StartTransaction(StartLevel);
    }
    private void OnTutorialClick()
    {
        GameInstance.Instance.UI.StartTransaction(StartTutorial);
    }
    private void OnExitClick()
    {
#if !UNITY_WEBGL
        GameInstance.Instance.UI.StartTransaction(Application.Quit);
#endif
    }
    private void StartLevel()
    {
       
        GameInstance.Instance.ChangeScene("Level0");
    }
    private void StartTutorial()
    {

        GameInstance.Instance.ChangeScene("Tutorial");
    }
    private void StartPrologue()
    {
        GameInstance.Instance.ChangeScene("Prologue");

    }
}
