using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    [SerializeField]
    private float selfDestructTimer = 3f;

    private void Start() => Destroy(gameObject, selfDestructTimer);
}