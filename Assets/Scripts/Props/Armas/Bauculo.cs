using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bauculo : MonoBehaviour
{

    public GameObject bullet;
    public float speed;
    private Transform firepoint;
    //Camera
    public Camera mainCamera;

    void Start()
    {
        firepoint = GameObject.FindGameObjectWithTag("Hand").transform;
    }
    private void Update()
    {
        firepoint.rotation = mainCamera.transform.rotation;
    }
    public void Fire()
    {
        Instantiate (bullet, firepoint.position, firepoint.rotation);
        Debug.DrawRay(firepoint.position, firepoint.forward * 100, Color.red, 2f);
        Ray ray = new Ray(firepoint.position, firepoint.forward);
        //RaycastHit hitInfo;

        FindObjectOfType<AudioManager>().PlayRandomPitch("MagicShot");
    }
}
