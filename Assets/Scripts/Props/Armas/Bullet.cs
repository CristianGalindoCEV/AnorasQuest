    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 8f;
    private float f_timeCounter = 0f;
    private float dietime = 3f;

    void Update()
    {
     f_timeCounter += Time.deltaTime;

     if (speed != 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("No speed");
        }
     //Destroy bullet
     if (f_timeCounter > dietime){Destroy(gameObject);}
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyMele")
        {
            Destroy(gameObject);
        }
    }
}
