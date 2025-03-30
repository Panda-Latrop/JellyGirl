using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceGameState : GameState
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameCondition[] winConditions, failConditions;
    [SerializeField] private Timer delay = new Timer(1f);
    private bool inInit = false;
    public Character Player => playerController.Player;
    public GameCondition[] WinConditions => winConditions;
    public override void Init()
    {
        inInit = true;
        delay.Run();
        
    }
    private void LateUpdate()
    {
        if (inInit && delay.Check())
        {
            inInit = false;
            StartGame();

        }


        if (State == GameStateEnum.start)
        {
            if (Check(winConditions))
                WinGame();
            else if(Check(failConditions))
                OverGame();
        }
    }
    private bool Check(GameCondition[] conditions)
    {
        for (int i = 0; i < conditions.Length; i++)
        {
            if (!conditions[i].Check())
                return false;
        }
        return true;
    }
    public override void Pause()
    {
        base.Pause();
        GameInstance.Instance.UI.Change("pause");
    }
    public override void Resume()
    {
        base.Resume();
        GameInstance.Instance.UI.Change("process");
    }
    public override void OverGame()
    {
        base.OverGame();
        GameInstance.Instance.UI.Change("over");
        playerController.CanInput = false;
    }
    public override void WinGame()
    {
        base.WinGame();
        GameInstance.Instance.UI.Change("win");
        playerController.CanInput = false;
    }
}
