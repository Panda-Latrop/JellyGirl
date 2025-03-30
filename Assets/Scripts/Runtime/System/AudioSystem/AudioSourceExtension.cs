using UnityEngine;
public static class AudioSourceExtension
{
    public static void SetClip(this AudioSource audioSource, AudioCueScriptableObject audioCue)
    {
        audioCue.AppendTo(audioSource);
    }
}
