using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalItem : MonoBehaviour
{
    public PlayerController playercontroller;
    private bool m_firtsPortal = false;
    public GameObject loading;



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_firtsPortal = true;
            StartCoroutine(Play());
        }
    }
    IEnumerator Play()
    {

        loading.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("StaticBoss");
    }
}