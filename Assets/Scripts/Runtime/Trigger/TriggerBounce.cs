using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBounce : Trigger
{
    [SerializeField] private float power = 10f;
    [SerializeField] private bool isAddPower;
    protected override void HandleEnter(Character destructible)
    {
        Vector3 direction = destructible.Center - transform.position;
        if(isAddPower)  
            destructible.Movement.Momentum += direction * power;
        else 
            destructible.Movement.Momentum = direction * power;
        base.HandleEnter(destructible);
    }
}
