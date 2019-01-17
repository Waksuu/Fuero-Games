using UnityEngine;

[RequireComponent(typeof(SoundHandler))]
public class ShotBullet : MonoBehaviour
{
    public GameObject BulletPrefab;

    [Range(0f, 1.5f)]
    public float FireDelay = 0.5f;

    private float _cooldownTimer = 0.5f;
    private SoundHandler _soundHandler;

    private void Start()
    {
        _soundHandler = gameObject.GetComponent<SoundHandler>();
    }

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

    private void DecreaseCooldown() => _cooldownTimer -= Time.deltaTime;

    private void SetCooldown() => _cooldownTimer = FireDelay;

    private void PlayAudio() => _soundHandler.PlaySound(_soundHandler.AudioSource, _soundHandler.Audio, _soundHandler.AudioVolume);

    private void CreateBullet() => Instantiate(BulletPrefab, transform.position, transform.rotation);

    private bool CooldownEnded() => _cooldownTimer <= 0;
}