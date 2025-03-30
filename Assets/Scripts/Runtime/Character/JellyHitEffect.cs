using Management.Pool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class JellyHitEffect : MonoBehaviour
{
    [SerializeField] private Transform root;
    [SerializeField] private Character character;
    [SerializeField] private DynamicEffect[] effectsPrefab;
    [SerializeField] private AudioSource sourceHit,sourcePick;
    [SerializeField] private AudioCueScriptableObject hitCue,pickCue;
    protected virtual void Awake()
    {
        character.OnHurt += OnCharacterHurt;
        character.OnHeal += OnCharacterHeal;
        character.Collider.OnCollisionEnter += OnCharacterCollision;

    }
    protected virtual void OnDestroy()
    {
        character.OnHurt -= OnCharacterHurt;
        character.OnHeal -= OnCharacterHeal;
        character.Collider.OnCollisionEnter -= OnCharacterCollision;
    }
    protected virtual void OnCharacterHurt(HurtStruct obj)
    {
        if (obj.damage.type != DamageType.none)
        {
            Execute();
        }
    }
    private void OnCharacterCollision(Collision2D collision)
    {
        if (character.Movement.Momentum.sqrMagnitude >= 4f)
        {
            Execute();
        }

    }
    private void OnCharacterHeal(HealStruct obj)
    {
        pickCue.PlayTo(sourcePick);

    }
    private void Execute()
    {
        for (int i = 0; i < effectsPrefab.Length; i++)
        {
            var effectPrefab = effectsPrefab[i];
            IPoolObject effect;
            if (effectPrefab.IsInherit)
                effect = GameInstance.Instance.Pool.Pop(effectPrefab, root);
            else
                effect = GameInstance.Instance.Pool.Pop(effectPrefab, root.position, Quaternion.identity);
            effect.Transform.localScale = Vector3.one * Mathf.Max(character.HealthPercent, 0.25f);
       
        }
         hitCue.PlayTo(sourceHit);
    }

}
