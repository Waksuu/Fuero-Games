using UnityEngine;

public class ShotBullet : MonoBehaviour
{
    public GameObject BulletPrefab;

    [Range(0f, 1.5f)]
    public float FireDelay = 0.5f;
    private float _cooldownTimer = 0.5f;

    private void Update()
    {
        _cooldownTimer -= Time.deltaTime;

        if (CooldownEnded())
        {
            Instantiate(BulletPrefab, transform.position, transform.rotation);
            _cooldownTimer = FireDelay;
        }
    }

    private bool CooldownEnded() => _cooldownTimer <= 0;
}