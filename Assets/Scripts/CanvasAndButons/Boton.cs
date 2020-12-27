using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Boton : MonoBehaviour
{
    private PlayerStats playerStats;

    void Start()
    {
        UnlockMouse();
        playerStats = FindObjectOfType<PlayerStats>();
    }

    public void PulsaRetry()
    {
        playerStats.revive = true;
        SceneManager.LoadScene("MainScene");
    }

    public void PulsaExit()
    {
        Application.Quit();
    }
    public void PulsaPlay()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
