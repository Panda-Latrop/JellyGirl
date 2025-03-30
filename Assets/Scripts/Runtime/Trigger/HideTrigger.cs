using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideTrigger : MonoBehaviour
{
    [SerializeField] private Trigger trigger;
    [SerializeField] private GameObject[] targets;
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
        for (int i = 0; i < targets.Length; i++)
        {
            targets[i].SetActive(false);
        }
    }
}
