﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool gameispaused = false;

    public GameObject pausemenuUI;
    public GameObject ingameMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameispaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pausemenuUI.SetActive(false);
        ingameMenu.SetActive(false);

        Time.timeScale = 1f;
        gameispaused = false;
        
    }

    public void Pause()
    {
        pausemenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameispaused = true;
    }
}
