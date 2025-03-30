using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPopAnimation : AnimationAction
{
    [SerializeField] private Transform target;
    [SerializeField] private float scale = 0.5f;
    protected override void Action(float percent)
    {
        target.localScale = Vector3.LerpUnclamped(Vector3.one * scale, Vector3.one, percent);
    }
}
