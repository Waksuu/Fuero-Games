using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour
{
    public float SelfDestructTimer = 3f;
    void Start()
    {
        Destroy(gameObject, SelfDestructTimer);
    }
}
