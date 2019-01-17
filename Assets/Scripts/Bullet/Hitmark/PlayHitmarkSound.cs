using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SoundHandler))]
public class PlayHitmarkSound : MonoBehaviour
{
    private SoundHandler soundHandler;

    void Start()
    {
        soundHandler = gameObject.GetComponent<SoundHandler>();
        soundHandler.PlaySound(soundHandler.audioSource, soundHandler.Audio, soundHandler.AudioVolume);
    }

}
