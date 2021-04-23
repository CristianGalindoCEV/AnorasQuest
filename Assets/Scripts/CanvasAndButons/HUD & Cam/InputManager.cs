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
    private AudioListener mainListner;
    private AudioListener aimListner;

    //Player
    private PlayerController m_playerController;
    public PlayerStats playerStats;
    //Cursor
    private bool m_islocked;

    // Start is called before the first frame update
    void Start()
    {
        m_playerController = FindObjectOfType<PlayerController>();
        
        //Canvas
        menuon = false;
        pausemenu.SetActive(false);
        mirilla.SetActive(false);

        //Cursor
        Cursor.visible = (false);
        Cursor.lockState = CursorLockMode.Locked;

        //Listeners
        aimListner = GameObject.Find("AimCamera").GetComponent<AudioListener>();
        mainListner = GameObject.Find("MainCamera").GetComponent<AudioListener>();
    }

    // Update is called once per frame
    void Update()
    {
        f_cadence += Time.deltaTime;
        
        
        if (Input.GetButton("Fire2") && pauseManager.paused == false)
        {
            aimListner.enabled = true;
            mainListner.enabled = false;

            m_playerController.mainCamera.enabled = false;
            m_playerController.aimCamera.enabled = true;
            
            m_playerController.aiming = true;
            mirilla.SetActive(true);
        } //Aiming
        else
        {
            aimListner.enabled = false;
            mainListner.enabled = true;

            m_playerController.mainCamera.enabled = true;
            m_playerController.aimCamera.enabled = false;
            
            m_playerController.aiming = false;
            mirilla.SetActive(false);
        }
        if (Input.GetButtonDown("Fire1") && pauseManager.paused == false && f_cadence > playerStats.timeShot 
            && m_playerController.aiming == true)//Shoot
        {
            gunspawn.Fire();
            f_cadence = 0f;
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
