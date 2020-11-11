using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMainMenu : MonoBehaviour
{
    public static bool gameispaused = false;

    public CanvasMainMenu canvasmainmenu;

    // Update is called once per frame
    void Update()
    {
        if (canvasmainmenu.b_optionson == true)
        {
            Pause();
        }
        else
            Resume();
        
            
    }

    void Resume()
    {
        //pausemenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameispaused = false;
    }

    void Pause()
    {
        //pausemenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameispaused = true;
    }
}
