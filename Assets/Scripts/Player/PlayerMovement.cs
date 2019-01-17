using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlayerMovement : MonoBehaviour
{
    private const string _verticalAxis = "Vertical";
    private const string _horizontalAxis = "Horizontal";

    [Range(0f, 10f)]
    public float SpeedScale = 4f;

    [Range(0f, 400f)]
    public float RotationScale = 180f;

    private void Update()
    {
        var playerVerticalSpeed = Input.GetAxis(_verticalAxis);
        var playerRotationSpeed = Input.GetAxis(_horizontalAxis);

        var playerRotation = GetPlayerRotation(RotationScale, playerRotationSpeed);
        RotatePlayer(playerRotation);

        if (PlayerMovesForward(playerVerticalSpeed))
        {
            MovePlayerForward(SpeedScale, playerVerticalSpeed, playerRotation);
        }
    }

    #region HorizontalMovement

    private Quaternion GetPlayerRotation(float rotationScale, float playerRotationSpeed)
    {
        Quaternion rotation = transform.rotation;
        float rotationDirection = rotation.eulerAngles.z;

        rotationDirection += CaculatePlayerRotation(rotationScale, playerRotationSpeed);
        rotation = Quaternion.Euler(0, 0, rotationDirection);
        return rotation;
    }

    private float CaculatePlayerRotation(float rotationScale, float playerRotationSpeed) => rotationScale * playerRotationSpeed * Time.deltaTime;

    private void RotatePlayer(Quaternion playerRotation) => transform.rotation = playerRotation;

    #endregion HorizontalMovement

    #region VerticalMovement

    private bool PlayerMovesForward(float playerVerticalSpeed) => playerVerticalSpeed > 0;

    private void MovePlayerForward(float speedScale, float playerVerticalSpeed, Quaternion playerRotation)
    {
        Vector3 position = transform.position;

        var playerPosition = CaculatePlayerPosition(speedScale, playerVerticalSpeed);
        Vector3 velocity = new Vector3(0, playerPosition, 0);

        position += playerRotation * velocity;
        transform.position = position;
    }

    private float CaculatePlayerPosition(float speedScale, float playerVerticalSpeed) => speedScale * playerVerticalSpeed * Time.deltaTime;

    #endregion VerticalMovement
}