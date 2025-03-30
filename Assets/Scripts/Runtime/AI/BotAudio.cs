using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BotAudio : MonoBehaviour
{
    [SerializeField] private BotController controller;
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioCueScriptableObject audioCue;
    private void Awake()
    {
        controller.OnTarget += OnTarget;
    }
    private void OnDestroy()
    {
        controller.OnTarget -= OnTarget;
    }
    private void OnTarget()
    {
        source.loop = false;
        audioCue.PlayTo(source);
    }
}