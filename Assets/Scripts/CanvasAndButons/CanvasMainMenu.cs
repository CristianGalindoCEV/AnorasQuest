using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasMainMenu : MonoBehaviour
{

    public GameObject options;
    public GameObject mainmenu;
    public GameObject loading;
    public CanvasGroup loadingGroup;
    public bool b_optionson = false;

    public Animator tranistion;

    //Fade
    [SerializeField] private float f_time = 1;
    private bool b_Faded= false;

    // Start is called before the first frame update
    void Start()
    {
        mainmenu.SetActive(true);
        options.SetActive(false);
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
        
        tranistion.SetBool("PressPlay" ,true);
        mainmenu.SetActive(false);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("MainScene");
    }
}
