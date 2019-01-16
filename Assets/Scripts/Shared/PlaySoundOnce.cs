using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySoundOnce : MonoBehaviour
{
    public AudioClip Audio;
    [Range(0f, 1f)]
    public float AudioVolume = 0.6f;

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlaySound(audioSource, Audio, AudioVolume);
    }

    private void PlaySound(AudioSource audioSource, AudioClip audio, float audioVolume)
    {
        audioSource.PlayOneShot(audio, audioVolume);
    }
}
