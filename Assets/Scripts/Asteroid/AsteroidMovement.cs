using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private float randomSpeed;

    private void Awake() => randomSpeed = Random.Range(10f, 100f);

    private void Start()
    {
        Rigidbody2D asteroidBody = GetComponent<Rigidbody2D>();

        //Set asteroid in motion
        asteroidBody.AddForce(transform.up * randomSpeed);
    }
}