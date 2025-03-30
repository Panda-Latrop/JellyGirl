using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class TriggerHeal : Trigger
{
    [SerializeField,Min(0)] private float ammount = 25f;
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
        destructible.Heal(new HealStruct(ammount,HealType.self,0,transform.position));
        base.HandleEnter(destructible);
    }
}
