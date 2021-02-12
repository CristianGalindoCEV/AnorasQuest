using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private float f_TimeCounter = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        f_TimeCounter += Time.deltaTime;        
        transform.Translate(Vector3.forward * Time.deltaTime*15);
        if (f_TimeCounter > 3){Destroy(gameObject);}
    }
}
