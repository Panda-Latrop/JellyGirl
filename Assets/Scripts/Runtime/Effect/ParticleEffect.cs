using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : DynamicEffect
{
    [SerializeField] private ParticleSystem effect;
    public override void Execute()
    {
        effect.Play();
    }

    public override void Stop()
    {
        effect.Stop();
    }
}
