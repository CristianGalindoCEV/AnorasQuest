using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class CanvasMainMenu : MonoBehaviour
{

    public GameObject options;
    public GameObject mainmenu;
    public GameObject loading;
    public bool b_optionson = false;


    //Loading Animator
    public Animator transtion;

    //loading random
    public int i_number;
    private GameObject m_text1;
    private GameObject m_text2;
    private GameObject m_text3;
    private GameObject m_image1;
    private GameObject m_image2;
    private GameObject m_image3;

    //FadeOut
    public AudioMixerSnapshot paused;
    public AudioMixerSnapshot nopaused;
    // Start is called before the first frame update
    void Start()
    {
        mainmenu.SetActive(true);
        options.SetActive(false);

        m_text1 = GameObject.Find("Text 1");
        m_text2 = GameObject.Find("Text 2");
        m_text3 = GameObject.Find("Text 3");
        m_image1 = GameObject.Find("Image 1");
        m_image2 = GameObject.Find("Image 2");
        m_image3 = GameObject.Find("Image 3");
        

        m_image1.SetActive(false);
        m_text1.SetActive(false);
        m_image2.SetActive(false);
        m_text2.SetActive(false);
        m_image3.SetActive(false);
        m_text3.SetActive(false);

       FindObjectOfType<AudioManager>().Play("MenuBGM");
    }


    public void PulsaOpciones()
    {
        options.SetActive (true);
        mainmenu.SetActive(false);
        b_optionson = true;
        FindObjectOfType<AudioManager>().Play("Button");
    }
    public void PulsaBack()
    {
        options.SetActive(false);
        mainmenu.SetActive(true);
        b_optionson = false;
        FindObjectOfType<AudioManager>().Play("Button");
    }
    public void PulsaExit()
    {
        Application.Quit();
    }
    public void PulsaCredits()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        SceneManager.LoadScene("Credits");
    }
    public void PulsaPlay()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        StartCoroutine(Play());
    }


    IEnumerator Play()
    {

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

        transtion.SetBool("PressPlay" ,true);
        mainmenu.SetActive(false);
        paused.TransitionTo(4f);
        
        yield return new WaitForSeconds(4);
        
        nopaused.TransitionTo(0.1f);
        FindObjectOfType<AudioManager>().Stop("MenuBGM");
        SceneManager.LoadScene("MainScene");
    }
}
