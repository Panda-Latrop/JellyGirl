using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIGameCondition : UIElement
{
    private GameCondition condition;
    [SerializeField] private AnimationAction anim;
    [SerializeField] private TMPro.TMP_Text info;

    public void Init()
    {
        condition = (GameInstance.Instance.GameState as RaceGameState).WinConditions[0];
        condition.OnChange += OnConditionChange;
        OnConditionChange(condition.Current);
    }
    private void OnDestroy()
    {
        condition.OnChange -= OnConditionChange;
    }
    private void OnConditionChange(int current)
    {
        info.SetText($"{current}/{condition.Max}");
        if(GameInstance.Instance.GameState.State != GameStateEnum.none)
        anim.Play();
    }


}
