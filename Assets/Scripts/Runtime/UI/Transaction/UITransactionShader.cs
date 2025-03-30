using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class UITransactionShader : UITransaction
{
    [SerializeField] private Image background;
    private Material material;
    private const string FADE_PROPERTY = "_ForegroundCutoff";
    public override void Init()
    {
        material = background.material;
    }
    protected override void Execute()
    {
        if(inReverse)
            material.SetFloat(FADE_PROPERTY, 0f);
        else
            material.SetFloat(FADE_PROPERTY, 1.35f);
        base.Execute();
    }
    protected override void Action(float percent)
    {
        material.SetFloat(FADE_PROPERTY, (1f-percent) * 1.35f);
    }
}
