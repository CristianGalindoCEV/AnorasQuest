﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalItem : MonoBehaviour
{
    public PlayerController playercontroller;
    private bool m_firtsPortal = false;
    public GameObject loading;
    public Animator tranistion;


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
        tranistion.SetBool("PressPlay", true);
        loading.SetActive(true);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("StaticBoss");
    }
}