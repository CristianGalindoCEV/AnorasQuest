﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bauculo : MonoBehaviour
{

    public GameObject bullet;
    public float speed;

    public void Fire()
    {
        Instantiate (bullet, transform.position, transform.rotation);
        FindObjectOfType<AudioManager>().PlayRandomPitch("MagicShot");
    }
}
