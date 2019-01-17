using UnityEngine;

[RequireComponent(typeof(SoundHandler))]
public class ShotBullet : MonoBehaviour
{
    public GameObject BulletPrefab;

    [Range(0f, 1.5f)]
    [SerializeField]
    private float fireDelay = 0.5f;

    private float cooldownTimer = 0.5f;
    private SoundHandler soundHandler;

    private void Start() => soundHandler = gameObject.GetComponent<SoundHandler>();

    private void Update()
    {
        if (CooldownEnded())
        {
            CreateBullet();
            PlayAudio();
            SetCooldown();
        }
        else
        {
            DecreaseCooldown();
        }
    }

    private void DecreaseCooldown() => cooldownTimer -= Time.deltaTime;

    private void SetCooldown() => cooldownTimer = fireDelay;

    private void PlayAudio() => soundHandler.PlaySound(soundHandler.AudioSource, soundHandler.Audio, soundHandler.AudioVolume);

    private void CreateBullet() => Instantiate(BulletPrefab, transform.position, transform.rotation);

    private bool CooldownEnded() => cooldownTimer <= 0;
}