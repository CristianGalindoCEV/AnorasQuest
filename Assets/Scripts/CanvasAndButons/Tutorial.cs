using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    public PlayerStats playerStats;
    public MenuManager menuManager;

    public TextMeshProUGUI ControlTutorial;
    public TextMeshProUGUI JumpTutorial;
    public TextMeshProUGUI ShotTutorial;

    private bool b_start = false;
    private bool b_move = false;
    private bool b_jump = false;
    private bool b_shot = false;

    // Start is called before the first frame update
    void Start()
    {
        
        //Tutorial
        ControlTutorial.enabled = false;
        JumpTutorial.enabled = false;
        ShotTutorial.enabled = false;

        if (playerStats.tutorial == false)
        {
            StartCoroutine(MoveTutorial());
        }
        if (playerStats.tutorial == true)
        {
            this.enabled = false;
        }
    }
    private void Update()
    {
        if(b_move == false && b_start == true) //MoveTutorial
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                StartCoroutine(MyJumpTutorial());
                StartTurotailShot();
            }
        }
        if(b_move == true && b_jump == false) //JumpTutorial
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(MyShotTutorial());
                Debug.Log("Shot");
            }
        }
        if(b_jump == true && b_shot == false) //ShotTutorial
        {
            if (Input.GetButtonDown("Fire2"))
            {
                StartCoroutine(MyEndTutorial());
            }
        }
    }

    private void StartTurotailShot()
    {
        b_move = true;
    }

    IEnumerator MoveTutorial()
    {
        //yield return new WaitForSeconds(1.5f);

        menuManager.UiTextAnimation();
        ControlTutorial.enabled = true;

        yield return new WaitForSeconds(0.5f);

        ControlTutorial.DOFade(1f, 1f).SetEase(Ease.OutQuint);

        yield return new WaitForSeconds(1f);

        b_start = true;
    }

    IEnumerator MyJumpTutorial()
    {
        yield return new WaitForSeconds(1.5f);

        menuManager.UiTextAnimation();

        JumpTutorial.enabled = true;
        ControlTutorial.enabled = false;

        yield return new WaitForSeconds(0.5f);

        JumpTutorial.DOFade(1f, 1f).SetEase(Ease.OutQuint);
    }
    IEnumerator MyShotTutorial()
    {
        yield return new WaitForSeconds(1.5f);

        menuManager.UiTextAnimation();

        ShotTutorial.enabled = true;
        JumpTutorial.enabled = false;

        yield return new WaitForSeconds(0.5f);

        ShotTutorial.DOFade(1f, 1f).SetEase(Ease.OutQuint);

        yield return new WaitForSeconds(1f);

        b_jump = true;
    }
    IEnumerator MyEndTutorial()
    {

        yield return new WaitForSeconds(2f);
        menuManager.Icon_L.enabled = false;
        menuManager.Icon_R.enabled = false;
        ShotTutorial.enabled = false;
        playerStats.tutorial = true;
    }
}
