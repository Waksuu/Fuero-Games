using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    private float randomSpeed;

    private void Awake() => randomSpeed = GenerateRandomSpeed();

    private void Start()
    {
        Rigidbody2D asteroidBody = GetComponent<Rigidbody2D>();
        SetAsteroidInMontion(asteroidBody, randomSpeed);
    }

    private float GenerateRandomSpeed() => Random.Range(10f, 100f);

    private void SetAsteroidInMontion(Rigidbody2D asteroidBody, float randomSpeed) => asteroidBody.AddForce(transform.up * randomSpeed);
}