#pragma warning disable 0649

using UnityEngine;

[RequireComponent(typeof(SoundHandler))]
public class ShotBullet : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;

    [Range(0f, 1.5f)]
    [SerializeField]
    private float fireDelay = 0.5f;

    private float cooldownTimer = 0.5f;
    private SoundHandler soundHandler;

    private void Start() => soundHandler = gameObject.GetComponent<SoundHandler>();

    private void Update()
    {
        if (CooldownEnded(cooldownTimer))
        {
            CreateBullet(bulletPrefab);
            soundHandler.PlaySound();
            SetCooldown(ref cooldownTimer, fireDelay);
        }
        else
        {
            DecreaseCooldown(ref cooldownTimer);
        }
    }

    private bool CooldownEnded(float cooldownTimer) => cooldownTimer <= 0;

    private void CreateBullet(GameObject bulletPrefab) => Instantiate(bulletPrefab, transform.position, transform.rotation);

    private void SetCooldown(ref float cooldownTimer, float fireDelay) => cooldownTimer = fireDelay;

    private void DecreaseCooldown(ref float cooldownTimer) => cooldownTimer -= Time.deltaTime;
}