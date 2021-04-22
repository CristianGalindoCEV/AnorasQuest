using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Boton : MonoBehaviour
{
    public PlayerStats playerStats;

    void Start()
    {
        UnlockMouse();
    }

    public void PulsaRetry()
    {
        playerStats.revive = true;
        Debug.Log("Ponte true");
        SceneManager.LoadScene("BIgLevel");
    }

    public void PulsaExit()
    {
        Application.Quit();
    }
    public void PulsaPlay()
    {
        SceneManager.LoadScene("BIgLevel");
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
