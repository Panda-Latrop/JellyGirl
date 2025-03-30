using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTrigger : MonoBehaviour
{
    [SerializeField] private Trigger trigger;
    [SerializeField] private Animator anim;
    [SerializeField] private CharacterSwapClass facilSwap; 
    private readonly int happyHash = Animator.StringToHash("Happy");
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
        anim.SetTrigger(happyHash);
        facilSwap.Show("Feed");
    }
}
