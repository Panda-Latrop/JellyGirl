using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TriggerHurt : Trigger
{

    [SerializeField] private DamageType type = DamageType.none;
    [SerializeField, Min(0)] private float ammount = 25f;
    [SerializeField] float power = 0f;
#if UNITY_EDITOR
    protected override void OnValidate()
    {
        base.OnValidate();
        if (!IsSingleExecute)
        {
            IsSingleExecute = true;
            EditorUtility.SetDirty(this);
        }
    }
#endif
    protected override void HandleEnter(Character destructible)
    {
        Vector3 diraction = destructible.Center - transform.position;
        destructible.Hurt(new DamageStruct(ammount, power, type, 0, transform.position, diraction),transform.position,-diraction);
        base.HandleEnter(destructible);
    }
}