using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SoundHandler))]
public class PlayHitmarkSound : MonoBehaviour
{
    private SoundHandler _soundHandler;

    void Start()
    {
        _soundHandler = gameObject.GetComponent<SoundHandler>();
        PlaySound();
    }

    private void PlaySound() => _soundHandler.PlaySound(_soundHandler.AudioSource, _soundHandler.Audio, _soundHandler.AudioVolume);
}
