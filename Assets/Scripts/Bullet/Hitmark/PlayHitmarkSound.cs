using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayHitmarkSound : MonoBehaviour
{
    public AudioClip HitmarkAudio;
    [Range(0f, 1f)]
    public float AudioVolume = 0.6f;

    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        PlayHitmarkSoundOnce(audioSource, HitmarkAudio, AudioVolume);
    }

    private void PlayHitmarkSoundOnce(AudioSource audioSource, AudioClip hitmarkAudio, float audioVolume)
    {
        audioSource.PlayOneShot(hitmarkAudio, audioVolume);
    }
}
