using UnityEngine;

[RequireComponent(typeof(SoundHandler))]
public class PlayHitmarkSound : MonoBehaviour
{
    private SoundHandler soundHandler;

    private void Start()
    {
        soundHandler = gameObject.GetComponent<SoundHandler>();
        soundHandler.PlaySound();
    }
}