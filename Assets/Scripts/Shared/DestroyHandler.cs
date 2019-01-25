using Assets.Enums;
using UnityEngine;

public class DestroyHandler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (DoesntCollideWithCamera(collision))
        {
            Destroy(gameObject);
        }
    }

    private bool DoesntCollideWithCamera(Collider2D collision) => collision?.gameObject?.layer != (int)Layers.Camera;
}