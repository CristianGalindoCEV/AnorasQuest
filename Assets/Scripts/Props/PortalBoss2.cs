﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PortalBoss2 : MonoBehaviour
{
    public PlayerController playercontroller;
    private GameObject loading;
    public Animator tranistion;
    public CanvasGroup hud;

    //MapPase
    public PlayerStats playerStats;
    public GameObject thisPortal;
    public GameObject myWall;
    public GameObject myButton;

    //Audio
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot nopaused;

    //loading random
    private int i_number;
    public GameObject m_text1;
    public GameObject m_text2;
    public GameObject m_text3;
    public GameObject m_image1;
    public GameObject m_image2;
    public GameObject m_image3;

    //GameObjects desaper if you win the boss
    public GameObject [] Crystals;

    private void Start()
    {
        loading = GameObject.Find("loadingScreen");

        m_image1.SetActive(false);
        m_text1.SetActive(false);
        m_image2.SetActive(false);
        m_text2.SetActive(false);
        m_image3.SetActive(false);
        m_text3.SetActive(false);

        if(playerStats.FlyBoss == true)
        {
            thisPortal.SetActive(false);
            myWall.SetActive(false);
            myButton.SetActive(false);

            for (int i = 0; i < Crystals.Length; i++)
            {
                Crystals[i].SetActive(false);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Play());
            //Meter Audio
        }
    }
    IEnumerator Play()
    {
        i_number = Random.Range(1, 3);

        switch (i_number)
        {
            case 1:
                m_image1.SetActive(true);
                m_text1.SetActive(true);
                break;

            case 2:
                m_image2.SetActive(true);
                m_text2.SetActive(true);
                break;

            case 3:
                m_image3.SetActive(true);
                m_text3.SetActive(true);
                break;

            default:
                Debug.Log("Switch error");
                break;
        }

        tranistion.SetBool("PressPlay", true);
        hud.alpha = 0;
        loading.SetActive(true);
        paused.TransitionTo(4f);

        yield return new WaitForSeconds(0.5f);
        
        //PlayerEneable
        playercontroller.speed = 0;
        playercontroller.player.enabled = false;
        
        yield return new WaitForSeconds(3f);
        nopaused.TransitionTo(0.1f);
        FindObjectOfType<AudioManager>().Stop("MenuBGM");
        SceneManager.LoadScene("FlyBoss");
    }
}
