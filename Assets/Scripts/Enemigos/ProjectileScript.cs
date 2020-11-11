using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private float TimeCounter = 0;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TimeCounter += Time.deltaTime;

        
        transform.Translate(Vector3.forward * Time.deltaTime*15);
        
        
       
        if (TimeCounter > 3){Destroy(gameObject);}
    }
}
