using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bauculo : MonoBehaviour
{

    public GameObject bullet;
    public float speed;


    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Fire()
    {
        Debug.DrawRay(transform.position, transform.up * 100, Color.red, 2f);
    }
}
