using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform MyTarget;

    private void Update() => SetCameraAtTargetPosition(MyTarget);

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