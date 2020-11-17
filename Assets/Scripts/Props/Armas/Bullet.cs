    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5;
    private float f_timeCounter = 0;
    [SerializeField] private float dietime;

    void Start()
    {
        
    }

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
}
