using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.MusicSystem;


public class MusicChanger : MonoBehaviour
{
    [SerializeField] private string specifer = "base";
    [SerializeField] private float musicPercentToChange = 0.9f;


    [SerializeField] private AudioClip[] clips;
    private MusicSystem music;
    private void Start()
    {
        music = GameInstance.Instance.Music;
        if (!string.Equals(music.Specigfer, specifer))
        {
            music.CrossFade(clips[Random.Range(0, clips.Length)]);
            music.Specigfer = specifer;
        }
    }

    private void LateUpdate()
    {
        if (music.Time >= music.Duration * musicPercentToChange)
        {
            int random = Random.Range(0, clips.Length);
            AudioClip clip = clips[random];
            if (music.CheckPlaying(clip))
            {
                random = (random + 1 + clips.Length) % clips.Length;
                clip = clips[random];
            }

            music.CrossFade(clip);
        }
    }
}