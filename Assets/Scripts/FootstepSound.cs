using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{


    /* private void OnTriggerEnter(Collider other)
     {
         FindObjectOfType<AudioManager>().PlayRandomPitch("Footstep");
         Debug.Log(":)");
     } */
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            FindObjectOfType<AudioManager>().PlayRandomPitch("Footstep");
            
        }
    }
}
