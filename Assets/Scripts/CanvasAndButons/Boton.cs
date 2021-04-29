using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Boton : MonoBehaviour
{
    public PlayerStats playerStats;
    public GameObject loadingScreen;
    public GameObject Interface;

    //Loading Animator
    public Animator transtion;

    //loading random

    [SerializeField] private int i_number;
    [SerializeField] private GameObject m_text1;
    [SerializeField] private GameObject m_text2;
    [SerializeField] private GameObject m_text3;
    [SerializeField] private GameObject m_image1;
    [SerializeField] private GameObject m_image2;
    [SerializeField] private GameObject m_image3;
    
    //FadeOut
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot nopaused;

    void Start()
    {
        UnlockMouse();
        
        m_text1 = GameObject.Find("Text_Anora");
        m_text2 = GameObject.Find("Text_Enemy");
        m_text3 = GameObject.Find("Text_FinalBoss");
        m_image1 = GameObject.Find("Image_Anora");
        m_image2 = GameObject.Find("Image_Enemy");
        m_image3 = GameObject.Find("Image_FinalBoss");

        m_image1.SetActive(false);
        m_text1.SetActive(false);
        m_image2.SetActive(false);
        m_text2.SetActive(false);
        m_image3.SetActive(false);
        m_text3.SetActive(false);

        loadingScreen.SetActive(false);
    }

    public void PulsaRetry()
    {
        StartCoroutine(Play());
    }

    public void PulsaExit()
    {
        Application.Quit();
    }

    public void ExitToMenu()
    {
        StartCoroutine(ExitMenu());
    }

    void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    IEnumerator Play()
    {
        loadingScreen.SetActive(true);
        playerStats.hp_stat = playerStats.maxhp_stat;
        //playerStats.bulletDamage_stat = 15;
        playerStats.playerPosition_stat = new Vector3(92, 11, 75);

        //Transition Scene
        i_number = Random.Range(1, 3);

        switch (i_number)
        {
            case 1:
                m_image1.SetActive(true);
                m_text1.SetActive(true);
                break;

            case 2:
                m_image2.SetActive(true);
                m_text2.SetActive(true);
                break;

            case 3:
                m_image3.SetActive(true);
                m_text3.SetActive(true);
                break;

            default:
                Debug.Log("Switch error");
                break;
        }

        transtion.SetBool("PressPlay", true);
        Interface.SetActive(false);
        //paused.TransitionTo(4f);
        Cursor.visible = false;
        yield return new WaitForSeconds(4);

        //nopaused.TransitionTo(0.1f);
        //FindObjectOfType<AudioManager>().Stop("MenuBGM");
        SceneManager.LoadScene("BIgLevel");
    }
    IEnumerator ExitMenu()
    {
        loadingScreen.SetActive(true);
        //Transition Scene
        i_number = Random.Range(1, 3);

        switch (i_number)
        {
            case 1:
                m_image1.SetActive(true);
                m_text1.SetActive(true);
                break;

            case 2:
                m_image2.SetActive(true);
                m_text2.SetActive(true);
                break;

            case 3:
                m_image3.SetActive(true);
                m_text3.SetActive(true);
                break;

            default:
                Debug.Log("Switch error");
                break;
        }

        transtion.SetBool("PressPlay", true);
        Interface.SetActive(false);
        //paused.TransitionTo(4f);
        Cursor.visible = false;
        yield return new WaitForSeconds(4);

        //nopaused.TransitionTo(0.1f);
        //FindObjectOfType<AudioManager>().Stop("MenuBGM");
        SceneManager.LoadScene("MainMenu");
    }
}
