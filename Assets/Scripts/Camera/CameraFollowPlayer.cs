using UnityEngine;
using System.Linq;

public class CameraFollowPlayer : MonoBehaviour
{
    private Transform myTarget;

    private void Update() => SetCameraAtPlayerPosition();

    private void SetCameraAtPlayerPosition()
    {
        if (myTarget != null)
        {
            Vector3 targetPosition = myTarget.position;
            targetPosition.z = transform.position.z;
            transform.position = targetPosition;
        }
        else
        {
            myTarget = GameObject.FindGameObjectsWithTag("Player").SingleOrDefault()?.transform;
        }
    }
}