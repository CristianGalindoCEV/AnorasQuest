using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    private float TimeCounter = 0;
    // Update is called once per frame
    void Update()
    {
        TimeCounter += Time.deltaTime;
        if (TimeCounter < 2){
            transform.Translate(Vector3.up * Time.deltaTime);
        
        }
       
        if (TimeCounter > 3){Destroy(gameObject);}
    
           
    }
}
