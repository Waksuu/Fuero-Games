using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    [Range(0f, 10f)]
    public float SpeedScale = 4f;

    private void Update() => MoveBulletForward(SpeedScale);

    private void MoveBulletForward(float speedScale)
    {
        Vector3 position = transform.position;

        var bulletPosition = CaculateBulletPosition(speedScale);
        Vector3 velocity = new Vector3(0, bulletPosition, 0);

        position += transform.rotation * velocity;
        transform.position = position;
    }

    private float CaculateBulletPosition(float speedScale) => speedScale * Time.deltaTime;
}