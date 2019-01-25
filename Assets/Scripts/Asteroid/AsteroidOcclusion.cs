using UnityEngine;

public class AsteroidOcclusion : MonoBehaviour
{
    private Renderer objectRenderer;

    private void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (objectRenderer.enabled != true)
        {
            objectRenderer.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) => objectRenderer.enabled = false;
}