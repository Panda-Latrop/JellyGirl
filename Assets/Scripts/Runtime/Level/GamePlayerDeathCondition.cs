using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayerDeathCondition : GameCondition
{
    public override int Current => character.IsAlive ? 0 :1 ;
    public override int Max => 1;
    [SerializeField] private Character character;
    public override bool Check()
    {
        return !character.IsAlive;
    }
}
