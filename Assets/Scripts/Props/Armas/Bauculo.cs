using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Bauculo : MonoBehaviour
{

    public GameObject bullet;
    public float speed;
    private Transform firepoint;
    //Camera
    public CinemachineVirtualCamera aimCamera;

    void Start()
    {
        firepoint = GameObject.FindGameObjectWithTag("Hand").transform;
    }
    private void Update()
    {
        firepoint.rotation = aimCamera.transform.rotation;
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
