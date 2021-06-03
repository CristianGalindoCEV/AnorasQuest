using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class Tutorial : MonoBehaviour
{
    public PlayerStats playerStats;
    public MenuManager menuManager;
    public InputManager inputManager;

    //Animation
    private bool b_animation = false;
    public PlayableDirector playableDirector;
    public CinemachineVirtualCamera firtsCamAnimation; // Firts cam animated intro
    public CinemachineVirtualCamera finalCamAnimation; // Last cam animated intro
    public CinemachineFreeLook playerCamera;
    public PlayerController playerController;
    public GameObject Player;
    public GameObject FakePlayer;
    public GameObject HUD;
    [SerializeField] private float f_animationTime;

    //Tutorial
    private bool b_start = false;
    private bool b_move = false;
    private bool b_jump = false;
    private bool b_shot = false;

    // Start is called before the first frame update
    void Start()
    {
        if (playerStats.tutorial == false) // Tutorial no complete
        {
            firtsCamAnimation.enabled = true; //Animation Camera
            Player.SetActive(false);
            HUD.SetActive(false);
            FakePlayer.SetActive(true);
            
            //Eneable cameras
            playerController.aimCamera.enabled = false;
            playerCamera.enabled = false;
            inputManager.animationPlayed = true; //Cant aim

            StartCoroutine(StartAnimation());
        }
        
        if (playerStats.tutorial == true)
        {
            //Hacer que se paaguen las camaras de animation
            firtsCamAnimation.enabled = false;
            FakePlayer.SetActive(false);
            b_animation = true;
            this.enabled = false;
        }
    }
    private void Update()
    {
        if (b_animation == true)
        {
            if (b_move == false && b_start == true) //MoveTutorial
            {
                if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    StartCoroutine(MyJumpTutorial());
                    StartTurotailShot();
                }
            }
            if (b_move == true && b_jump == false) //JumpTutorial
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    StartCoroutine(MyShotTutorial());
                }
            }
            if (b_jump == true && b_shot == false) //ShotTutorial
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    StartCoroutine(MyEndTutorial());
                }
            }
        }
    }

    //Animation Voids
    IEnumerator StartAnimation()
    {
        playableDirector.Play(); //Start animation

        yield return new WaitForSeconds(f_animationTime);
       
        playableDirector.Stop(); // Stop animation
        Player.SetActive(true);
        HUD.SetActive(true);
        
        StartCoroutine(MoveTutorial());

        FakePlayer.SetActive(false);
        finalCamAnimation.enabled = false;
        inputManager.animationPlayed = false; // Can Aim
        b_animation = true;
        firtsCamAnimation.enabled = false;
        playerController.aimCamera.enabled = true;
        playerCamera.enabled = true;
    }

    //Tutorial Voids
    private void StartTurotailShot()
    {
        b_move = true;
    }
    IEnumerator MoveTutorial()
    {
        //yield return new WaitForSeconds(1.5f);
        menuManager.UiTextAnimation();
        menuManager.ControlText.SetText("W, A, S ,D TO MOVE"); // Add Text

        yield return new WaitForSeconds(1.5f);

        b_start = true;
    }
    IEnumerator MyJumpTutorial()
    {
        yield return new WaitForSeconds(1.5f);
        menuManager.UiTextAnimation();
        menuManager.ControlText.SetText("SPACE for jump"); // Add Text 

        yield return new WaitForSeconds(0.5f);
    }
    IEnumerator MyShotTutorial()
    {
        b_jump = true;
        yield return new WaitForSeconds(1.5f);
        menuManager.UiTextAnimation();
        menuManager.ControlText.SetText("RIGHT CLICK to aim" + " " + "LEFT CLICK shoot ( only when aiming )"); // Add Text 
    }
    IEnumerator MyEndTutorial()
    {
        yield return new WaitForSeconds(2f);
        menuManager.ControlText.SetText("None"); // Add Text 
        menuManager.ControlText.enabled = false;
        menuManager.Icon_L.enabled = false;
        menuManager.Icon_R.enabled = false;
        menuManager.Background_Icon.enabled = false;
        playerStats.tutorial = true;
    }
}
