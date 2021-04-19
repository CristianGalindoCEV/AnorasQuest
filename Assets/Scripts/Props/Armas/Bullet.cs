using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Bauculo bauculo;
    private float f_timeCounter = 0f;
    private float dietime = 3f;

    private void Start()
    {
        bauculo = FindObjectOfType<Bauculo>();
    }
    void Update()
    {
     f_timeCounter += Time.deltaTime;

     if (bauculo.Bulletspeed != 0)
        {
            transform.Translate(Vector3.forward * bauculo.Bulletspeed * Time.deltaTime);
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
