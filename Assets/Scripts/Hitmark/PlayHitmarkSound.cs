using UnityEngine;

[RequireComponent(typeof(SoundHandler))]
public class PlayHitmarkSound : MonoBehaviour
{
    private SoundHandler soundHandler;

    private void Start()
    {
        soundHandler = gameObject.GetComponent<SoundHandler>();
        PlaySound(soundHandler);
    }

    public void PlaySound(SoundHandler soundHandler) => soundHandler.PlaySound(soundHandler.AudioSource, soundHandler.Audio, soundHandler.AudioVolume);
}