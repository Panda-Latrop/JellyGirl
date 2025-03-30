using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtMove : MonoBehaviour
{
    [SerializeField] private DestructibleObject destructible;
    [SerializeField] private CharacterMovement movement;
    [SerializeField] private float damage = 1f;
    private void Update()
    {
        if(movement.Momentum.sqrMagnitude >= 0.25f)
        {
            destructible.Hurt(new DamageStruct(damage * Time.deltaTime, 0f, DamageType.none, 0, movement.Position, Vector3.zero), movement.Position, Vector3.zero);
        }
    }
}
