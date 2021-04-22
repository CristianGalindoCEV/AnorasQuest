using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    public PlayerStats playerStats;
    public TextMeshProUGUI ControlTutorial;
    public TextMeshProUGUI JumpTutorial;
    public TextMeshProUGUI ShotTutorial;

    private bool b_move = false;
    private bool b_jump = false;
    private bool b_shot = false;

    // Start is called before the first frame update
    void Start()
    {
        ControlTutorial.enabled = false;
        JumpTutorial.enabled = false;
        ShotTutorial.enabled = false;

        if (playerStats.tutorial == false)
        {
            ControlTutorial.enabled = true;
            ControlTutorial.rectTransform.DOMoveY(220, 1f).SetEase(Ease.InSine);
        }
        if (playerStats.tutorial == true)
        {
            this.enabled = false;
        }
    }
    private void Update()
    {
        if(b_move == false) //MoveTutorial
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                StartCoroutine(MyJumpTutorial());
            }
        }
        if(b_move == true && b_jump == false) //JumpTutorial
        {
            if (Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(MyShotTutorial());
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

    IEnumerator MyJumpTutorial()
    {
        yield return new WaitForSeconds(1f);
        ControlTutorial.enabled = false;
        JumpTutorial.enabled = true;
        JumpTutorial.rectTransform.DOMoveY(220, 1f).SetEase(Ease.InSine).OnComplete(StartTurotailShot);
    }
    IEnumerator MyShotTutorial()
    {
        yield return new WaitForSeconds(1f);
        JumpTutorial.enabled = false;
        ShotTutorial.enabled = true;
        ShotTutorial.rectTransform.DOMoveY(220, 1f).SetEase(Ease.InSine);
        b_jump = true;
    }
    IEnumerator MyEndTutorial()
    {
        yield return new WaitForSeconds(1f);
        ShotTutorial.enabled = false;
        playerStats.tutorial = true;
    }
}
