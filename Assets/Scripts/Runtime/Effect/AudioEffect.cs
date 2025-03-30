using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffect : MonoBehaviour
{
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioCueScriptableObject aduioCue;

    private void OnEnable()
    {
        aduioCue.PlayTo(source);
    }
}
