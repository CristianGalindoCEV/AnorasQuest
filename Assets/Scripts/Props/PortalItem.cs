using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PortalItem : MonoBehaviour
{
    public PlayerController playercontroller;
    private GameObject loading;
    public Animator tranistion;
    public CanvasGroup hud;
    public GameObject bossName;

    //Audio
    public AudioMixerSnapshot nopaused;

    //loading random
    private int i_number;
    private GameObject m_text1;
    private GameObject m_text2;
    private GameObject m_text3;
    private GameObject m_image1;
    private GameObject m_image2;
    private GameObject m_image3;
    private void Start()
    {
        loading = GameObject.Find("loadingScreen");

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
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Play());
        }
    }
    IEnumerator Play()
    {
        i_number = Random.Range(1, 4);

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

        tranistion.SetBool("PressPlay", true);
        hud.alpha = 0;
        loading.SetActive(true);
        bossName.SetActive(false);

        yield return new WaitForSeconds(4);
        
        //AudioUnfade
        nopaused.TransitionTo(0.1f);
        FindObjectOfType<AudioManager>().Stop("MenuBGM");
        
        SceneManager.LoadScene("BIgLevel");
    }
}