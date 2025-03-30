using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : DestructibleObject
{
    [SerializeField] private BodyCollider coll;
    [SerializeField] private CharacterAnimation anim;
    [SerializeField] private CharacterMovement movement;
    [SerializeField] private CharacterAttack attack;
    [SerializeField] private CharacterSprint sprint;
    [SerializeField] private CharacterDash dash;
    public CharacterAnimation Animation => anim;
    public CharacterMovement Movement => movement;
    public CharacterAttack Attack => attack;
    public CharacterSprint Sprint => sprint;
    public CharacterDash Dash => dash;
    public BodyCollider Collider => coll;
    protected override void HurtHandler(HurtStruct hurt, Vector3 velocity = default)
    {
        velocity = hurt.damage.direction * hurt.damage.power;
        base.HurtHandler(hurt, velocity);
        if (velocity.sqrMagnitude > 0)
            movement.Momentum = velocity;

    }
    protected override void DeathHandler(HurtStruct hurt, Vector3 velocity = default)
    {
        base.DeathHandler(hurt, velocity);
        Stop();
    }
    private void Update()
    {
        bool inDash = dash.InDash;
        bool inSprint = sprint.InSprint;
        movement.CanMove = !inDash;
        sprint.CanSprint = !inDash;
    }
    public void Stop()
    {
        movement.Stop();
        sprint.Stop();
        dash.Stop();
    }
}
