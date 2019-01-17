using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float SelfDestructTimer = 3f;

    private void Start() => Destroy(gameObject, SelfDestructTimer);
}