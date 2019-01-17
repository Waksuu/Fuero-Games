using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    [Range(0f, 10f)]
    [SerializeField]
    private float speedScale = 4f;

    private int seed = 1;
    private Quaternion rotation;

    private void Start()
    {
        Random.InitState(seed);
        rotation = GetPlayerRotation(speedScale, Random.Range(1,4));
    }

    private void Update() => MoveBulletForward(speedScale);

    private void MoveBulletForward(float speedScale)
    {
        Vector3 position = transform.position;

        var bulletPosition = CaculateBulletPosition(speedScale);
        Vector3 velocity = new Vector3(0, bulletPosition, 0);

        position += rotation * velocity;
        transform.position = position;
    }

    private float CaculateBulletPosition(float speedScale) => speedScale * Time.deltaTime;

    private Quaternion GetPlayerRotation(float rotationScale, float playerRotationSpeed)
    {
        Quaternion rotation = transform.rotation;
        float rotationDirection = rotation.eulerAngles.z;

        rotationDirection += Random.Range(0, 1);
        rotation = Quaternion.Euler(0, 0, rotationDirection);
        return rotation;
    }
}
