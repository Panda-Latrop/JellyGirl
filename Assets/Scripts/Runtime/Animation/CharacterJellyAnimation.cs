using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJellyAnimation : CharacterAnimation
{
    [SerializeField] private float minSpeedToHit = 2f;
    private readonly int hitHash = Animator.StringToHash("Hit");
    
    protected override void Awake()
    {
        base.Awake();
        character.Collider.OnCollisionEnter += OnCharacterCollision;
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        character.Collider.OnCollisionEnter -= OnCharacterCollision;
    }
    private void OnCharacterCollision(Collision2D collision)
    {
        if(character.Movement.Momentum.sqrMagnitude >= minSpeedToHit * minSpeedToHit)
        anim.SetTrigger(hitHash);
    }
}
