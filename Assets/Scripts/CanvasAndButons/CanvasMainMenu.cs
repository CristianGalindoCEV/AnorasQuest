using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMainMenu : MonoBehaviour
{

    public GameObject options;
    public GameObject mainmenu;
    public GameObject loading;
    public bool b_optionson = false;


    //Loading Animator
    public Animator tranistion;

    //loading random
    private int i_number;
    private GameObject m_text1;
    private GameObject m_text2;
    private GameObject m_text3;
    private GameObject m_image1;
    private GameObject m_image2;
    private GameObject m_image3;


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
    }

    public void PulsaOpciones()
    {
        options.SetActive (true);
        mainmenu.SetActive(false);
        b_optionson = true;
    }
    public void PulsaBack()
    {
        options.SetActive(false);
        mainmenu.SetActive(true);
        b_optionson = false;
    }
    public void PulsaExit()
    {
        Application.Quit();
    }
    public void PulsaCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void PulsaPlay()
    {
        StartCoroutine(Play());
    }


    IEnumerator Play()
    {

        i_number = Random.Range(1, 3);

        if (i_number == 1)
        {
            m_image1.SetActive(true);
            m_text1.SetActive(true);
        }
        if (i_number == 2)
        {
            m_image2.SetActive(true);
            m_text2.SetActive(true);
        }
        if (i_number == 3)
        {
            m_image3.SetActive(true);
            m_text3.SetActive(true);
        }

        tranistion.SetBool("PressPlay" ,true);
        mainmenu.SetActive(false);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("MainScene");
    }
}
