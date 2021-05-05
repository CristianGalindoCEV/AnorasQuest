using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Tutorial : MonoBehaviour
{
    public PlayerStats playerStats;
    public TextMeshProUGUI ControlTutorial;
    public TextMeshProUGUI JumpTutorial;
    public TextMeshProUGUI ShotTutorial;

    public Image Icon_L;
    public Image Icon_R;


    private bool b_move = false;
    private bool b_jump = false;
    private bool b_shot = false;

    // Start is called before the first frame update
    void Start()
    {
        //UI
        Icon_L.enabled = false;
        Icon_R.enabled = false;
        
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
        if(b_move == false) //MoveTutorial
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                StartCoroutine(MyJumpTutorial());
                StartTurotailShot();
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

    IEnumerator MoveTutorial()
    {
        //Reset Positions
        ControlTutorial.enabled = true;
        Icon_L.enabled = false;
        Icon_R.enabled = false;

        Icon_L.rectTransform.position = new Vector3(910, 150, 0);
        Icon_R.rectTransform.position = new Vector3(910, 150, 0);

        yield return new WaitForSeconds(0.1f);

        //Start

        Icon_L.enabled = true;
        Icon_R.enabled = true;

        Icon_L.transform.DOMoveX(450, 2f).SetEase(Ease.OutQuint);
        Icon_R.transform.DOMoveX(1350, 2f).SetEase(Ease.OutQuint);
        
        yield return new WaitForSeconds(0.5f);
        
        ControlTutorial.DOFade(1f, 1f).SetEase(Ease.OutQuint);

    }

    IEnumerator MyJumpTutorial()
    {
        //Reset Positions
        
        Icon_L.enabled = false;
        Icon_R.enabled = false;

        Icon_L.rectTransform.position = new Vector3(910, 150, 0);
        Icon_R.rectTransform.position = new Vector3(910, 150, 0);

        yield return new WaitForSeconds(0.1f);
        
        //Start

        Icon_L.enabled = true;
        Icon_R.enabled = true;
        JumpTutorial.enabled = true;
        ControlTutorial.enabled = false;

        Icon_L.transform.DOMoveX(450, 2f).SetEase(Ease.OutQuint);
        Icon_R.transform.DOMoveX(1350, 2f).SetEase(Ease.OutQuint);

        yield return new WaitForSeconds(0.5f);

        JumpTutorial.DOFade(1f, 1f).SetEase(Ease.OutQuint);
    }
    IEnumerator MyShotTutorial()
    {
        //Reset Positions
        
        Icon_L.enabled = false;
        Icon_R.enabled = false;
        
        Icon_L.rectTransform.position = new Vector3 (910,150,0);
        Icon_R.rectTransform.position = new Vector3(910, 150, 0);

        yield return new WaitForSeconds(0.1f);
        
        //Start

        Icon_L.enabled = true;
        Icon_R.enabled = true;
        ShotTutorial.enabled = true;
        JumpTutorial.enabled = false;

        Icon_L.transform.DOMoveX(450, 2f).SetEase(Ease.OutQuint);
        Icon_R.transform.DOMoveX(1350, 2f).SetEase(Ease.OutQuint);

        yield return new WaitForSeconds(0.5f);

        ShotTutorial.DOFade(1f, 1f).SetEase(Ease.OutQuint);
        b_jump = true;
    }
    IEnumerator MyEndTutorial()
    {

        yield return new WaitForSeconds(1f);
        Icon_L.enabled = false;
        Icon_R.enabled = false;
        ShotTutorial.enabled = false;
        playerStats.tutorial = true;
    }
}
