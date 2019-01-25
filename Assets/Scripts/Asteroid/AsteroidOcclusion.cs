using UnityEngine;

public class AsteroidOcclusion : MonoBehaviour
{
    private Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        renderer.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (renderer.enabled != true)
        {
            renderer.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) => renderer.enabled = false;
}