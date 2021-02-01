﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PortalItem : MonoBehaviour
{
    public PlayerController playercontroller;
    public GameMaster gameMaster;
    private GameObject loading;
    public Animator tranistion;
    public CanvasGroup hud;

    [SerializeField]private bool portal2;
    [SerializeField]private bool portal3;
    
    //Audio
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot nopaused;

    private void Start()
    {
        loading = GameObject.Find("loadingScreen");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Play());
        }
    }
    IEnumerator Play()
    {
        tranistion.SetBool("PressPlay", true);
        hud.alpha = 0;
        loading.SetActive(true);
        gameMaster.SavePlayerStats();
        paused.TransitionTo(4f);
       
        yield return new WaitForSeconds(4);
        nopaused.TransitionTo(0.1f);
        FindObjectOfType<AudioManager>().Stop("MenuBGM");
        if (portal2 == true)
        {
            SceneManager.LoadScene("FlyBoss");
        } else if (portal3 == true)
        {
            SceneManager.LoadScene("Credits");
        }
        SceneManager.LoadScene("StaticBoss");
    }
}