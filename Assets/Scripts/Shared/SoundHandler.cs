using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundHandler : MonoBehaviour
{
    public AudioClip Audio;

    [Range(0f, 1f)]
    public float AudioVolume = 0.6f;

    public AudioSource AudioSource;

    public void PlaySound(AudioSource audioSource, AudioClip audio, float audioVolume) => audioSource.PlayOneShot(audio, audioVolume);
}