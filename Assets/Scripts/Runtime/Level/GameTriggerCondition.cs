using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTriggerCondition : GameCondition
{
    [SerializeField] private Trigger[] triggers;
    private int count;
    public override int Current 
    {
        get 
        {
            return count;
        } 
    }
    public override int Max => triggers.Length;
    public override bool Check()
    {
        var active = 0;
        for (int i = 0; i < triggers.Length; i++)
        {
            if (triggers[i].ExecutionCount > 0)
                active++;
        }
        if(active != count)
        {
            count = active;
            Execute();
        }
        return Current >= Max;
    }
}
