using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Scriptable/Audio Cue", fileName = "New Audio Cue")]
public class AudioCueScriptableObject : ScriptableObject
{
    [System.Serializable]
    public class AudioCueClipOption
    {
        [SerializeField] private AudioClip clip;
        [SerializeField, Range(0f, 1f)] float volume = 1f;
        public AudioClip Clip => clip;
        public float Volume => volume;
    }
    [SerializeField] private AudioCueClipOption[] options;
    [SerializeField] private float minPitch = 0.9f, maxPitch = 1.1f;
    [SerializeField] private AudioMixerGroup mixerGroup;

    public AudioCueClipOption this[int id] { get => options[id]; }
    public int Count => options.Length;
    public AudioCueClipOption Options => options[Random.Range(0, options.Length)];
    public float Pitch => Random.Range(minPitch, maxPitch);
    public AudioMixerGroup AudioMixerGroup => mixerGroup;
    public void AppendTo(AudioSource source, bool usePitch = true)
    {
        var option = Options;
        source.clip = option.Clip;
        source.volume = option.Volume;
        if (usePitch)
            source.pitch = Pitch;
        else
            source.pitch = 1f;
        source.outputAudioMixerGroup = mixerGroup;
    }
    public void PlayTo(AudioSource source, float volumeScale = 1f, bool usePitch = true)
    {
        AppendTo(source, usePitch);
        source.volume *= volumeScale;
        source.Play();
    }
}
