using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectTrigger : MonoBehaviour
{
    [SerializeField] private Trigger trigger;
    [SerializeField] private DynamicEffect effectPrefab;
    [SerializeField] private Vector3 offset;
    private void Awake()
    {
        trigger.OnTriggerEnter += Execute;
    }
    private void OnDestroy()
    {
        trigger.OnTriggerEnter -= Execute;
    }
    private void Execute(Character character)
    {
        var effect = GameInstance.Instance.Pool.Pop(effectPrefab, trigger.transform.position + offset, Quaternion.identity);

    }
}
