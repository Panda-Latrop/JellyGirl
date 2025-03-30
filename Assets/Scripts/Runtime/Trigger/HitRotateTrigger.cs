using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTrigger : MonoBehaviour
{
    [SerializeField] private Trigger trigger;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 rotation;
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
        float s = Mathf.Sign((character.Center.x- transform.position.x));
        target.localRotation = Quaternion.Euler(s*rotation);
    }
}
