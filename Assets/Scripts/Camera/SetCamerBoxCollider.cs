using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCamerBoxCollider : MonoBehaviour
{
    BoxCollider2D cameraCollider;
    private void Awake()
    {
        cameraCollider = GetComponent<BoxCollider2D>();
        cameraCollider.size = GetScreenSize(); 
    }

    private Vector3 GetScreenSize() => Camera.main.ViewportToWorldPoint(new Vector3(1, 1)) * 2;
}
