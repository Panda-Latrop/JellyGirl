using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class AudioTrigger : MonoBehaviour
{
    [SerializeField] private Trigger trigger;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioCueScriptableObject audioCue;
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
        source.loop = false;
        audioCue.PlayTo(source);
    }
}

