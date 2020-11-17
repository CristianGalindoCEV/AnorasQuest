using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalItem : MonoBehaviour
{
    public PlayerController playercontroller;
    private bool m_firtsPortal;



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            SceneManager.LoadScene("StaticBoss");
            m_firtsPortal = true;
        }
    }
}