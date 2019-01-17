using UnityEngine;

[RequireComponent(typeof(SoundHandler))]
public class PlayHitmarkSound : MonoBehaviour
{
    private SoundHandler soundHandler;

    private void Start()
    {
        soundHandler = gameObject.GetComponent<SoundHandler>();
        PlaySound();
    }

    private void PlaySound() => soundHandler.PlaySound(soundHandler.AudioSource, soundHandler.Audio, soundHandler.audioVolume);
}