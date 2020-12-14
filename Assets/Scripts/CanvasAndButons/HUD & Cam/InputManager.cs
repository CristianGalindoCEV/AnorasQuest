using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
   //Armas
    [SerializeField] private GameObject Gun;
    [SerializeField] private GameObject Espada;
    public bool bauculoItem = false;
    public bool swordItem = true;
    public Bauculo gunspawn;
    private float f_timetospawn = 0;
   
    //HUD
    public GameObject hud;
    public GameObject pausemenu;
    public GameObject mirilla;
    public bool menuon;
    public GameMaster gamemaster;
    public MenuManager menumanager;

    //Player
    private PlayerController m_player;

    //Cursor
    private bool m_islocked;


    // Start is called before the first frame update
    void Start()
    {
        Gun.SetActive(false);
        m_player = FindObjectOfType<PlayerController>();
        
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
        //Combate y armas

        
        if (Input.GetKey("1"))
        {
            swordItem = true;
            bauculoItem = false;
            ChangeWeapon();
            mirilla.SetActive(false);
        }
        
        if (Input.GetKey("2") && gamemaster.unlocked == true)
         {
            bauculoItem = true;
            swordItem = false;
            ChangeWeapon();
            mirilla.SetActive(true);
         }

        if (Input.GetButtonDown("Fire1") && bauculoItem == true)
        {
            gunspawn.Fire();
            FindObjectOfType<AudioManager>().Play("MagicShot");
        }

        if (Input.GetButtonDown("Fire1") && swordItem == true)
        {
            m_player.PlayerMeleAttack();
        }

        //Salto
        /*
            if (Input.GetButtonDown("Jump"))
                m_player.JumpStart();

            if (Input.GetButtonUp("Jump"))
                m_player.JumpRelased();
        */
        if (Input.GetButton("Jump"))
            m_player.Jump();

        //Dash
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            m_player.CastDash();
        }
        //Sprint
        if (Input.GetKey(KeyCode.LeftShift))
        {
            m_player.sprinting = true;
            m_player.Run();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            m_player.sprinting = false;
        }

        //GOOD MODE
        if (Input.GetKeyUp(KeyCode.F10))
        {
            if (m_player.god == true)
            {
                m_player.god = false;
                m_player.NoGod();
            }
            else
            {
                m_player.god = true;
                m_player.God();
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

    public void ChangeWeapon()
    {
        if (bauculoItem == true)
        {
            Espada.SetActive(false);
            Gun.SetActive(true);
            swordItem = (false);
            if (Input.GetButtonDown("Fire1"))
            {
                //Bauculo.Fire();
            }
        }

        else if (swordItem == true)
        {
            Espada.SetActive(true);
            Gun.SetActive(false);
            bauculoItem = (false);
            if (Input.GetButtonDown("Fire1"))
            {
                //Espada.Fire();
            }
        }
    }
}
