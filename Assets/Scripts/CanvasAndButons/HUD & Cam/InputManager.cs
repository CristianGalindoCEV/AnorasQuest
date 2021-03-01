using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
   //Armas
    public Bauculo gunspawn;
    private float f_cadence = 1f;
   
    //HUD
    public GameObject hud;
    public GameObject pausemenu;
    public GameObject mirilla;
    public bool menuon;
    public MenuManager menumanager;
    public PauseManager pauseManager;

    //Player
    private PlayerController m_playerController;

    //Cursor
    private bool m_islocked;

    // Start is called before the first frame update
    void Start()
    {
        m_playerController = FindObjectOfType<PlayerController>();
        
        //Canvas
        menuon = false;
        pausemenu.SetActive(false);

        //Cursor
        Cursor.visible = (false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        f_cadence += Time.deltaTime;
        if (Input.GetButtonDown("Fire1") && pauseManager.paused == false && f_cadence > 1f)
        {
            gunspawn.Fire();
            f_cadence = 0f;
        }
        
        //Dash
        if (Input.GetKeyDown(KeyCode.LeftControl) && pauseManager.paused == false && m_playerController.player.isGrounded)
        {
            m_playerController.CastDash();
        }
        
        //GOOD MODE
        if (Input.GetKeyUp(KeyCode.F10))
        {
            if (m_playerController.god == true)
            {
                m_playerController.god = false;
                m_playerController.NoGod();
            }
            else
            {
                m_playerController.god = true;
                m_playerController.God();
            }
        }
        
        //Canvas
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if( menuon == false)
            {
                pausemenu.SetActive(true);
                menuon = true;
                menumanager.panelgraphics.SetActive(false);
                menumanager.panelresolution.SetActive(false);
                menumanager.panelsound.SetActive(false);
                menumanager.optionsmenu.SetActive(false);
            }
            else
            {
                pausemenu.SetActive(false);
                menuon = false;
            }
        }
        //Cursor
        if (menuon == true)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = (true);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = (false);
        }
    }
}
