using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0f, 150f)]
    private float speed = 90f;
    private void Start()
    {
        Rigidbody2D bulletBody2D = GetComponent<Rigidbody2D>();
        SetBulletInMotion(bulletBody2D, speed);
    }

    private void SetBulletInMotion(Rigidbody2D bulletBody2D, float speed) => bulletBody2D.AddForce(transform.up * speed);
}