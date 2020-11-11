using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Boton : MonoBehaviour
{
    
    void Start()
    {
        UnlockMouse();
    }
    
    public void PulsaRetry()
    {
        SceneManager.LoadScene("EscenarioFinal");
    }

    public void PulsaExit()
    {
        Application.Quit();
    }
    public void PulsaPlay()
    {
        SceneManager.LoadScene("EscenarioFinal");
    }
    void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
