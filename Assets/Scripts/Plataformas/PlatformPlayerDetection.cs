using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlayerDetection : MonoBehaviour
{

    private Transform m_myTransform;
    private bool b_playerenter;
    private void Start()
    {
        m_myTransform = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("aa");
        if (other.tag == "Player" && b_playerenter == false)
        {

            other.transform.parent = m_myTransform;
            b_playerenter = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("pooo");
        if (other.tag == "Player" && b_playerenter == true)
        {
            other.transform.parent = null;
            b_playerenter = false;
        }
    }
}
