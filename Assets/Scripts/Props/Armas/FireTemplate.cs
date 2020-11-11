using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FireTemplate : MonoBehaviour
{
    public float timeDestroy = 1.0f;

    public abstract void Fire();


    public abstract void DestroySelf();

    private void Awake()
    {
        Invoke("DestroySelf", timeDestroy);
    }
}

