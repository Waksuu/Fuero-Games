using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform myTarget;

    private void Update() => SetCameraAtTargetPosition(myTarget);

    private void SetCameraAtTargetPosition(Transform myTarget)
    {
        if (myTarget != null)
        {
            Vector3 targetPosition = myTarget.position;
            targetPosition.z = transform.position.z;
            transform.position = targetPosition;
        }
    }
}