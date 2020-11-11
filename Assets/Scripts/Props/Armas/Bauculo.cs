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

    public void SpawnVFX()
    {
        GameObject instBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
        Rigidbody instbulletrigidbody = instBullet.GetComponent<Rigidbody>();
        instbulletrigidbody.AddForce(Vector3.forward * speed);
    }

}
