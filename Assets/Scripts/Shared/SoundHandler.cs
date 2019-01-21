using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundHandler : MonoBehaviour
{
    public AudioClip Audio;
    public AudioSource AudioSource;

    [Range(0f, 1f)]
    public float AudioVolume = 0.6f;

    public void PlaySound() => AudioSource.PlayOneShot(Audio, AudioVolume);
}