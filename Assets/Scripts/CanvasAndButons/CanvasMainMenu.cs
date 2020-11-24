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


    // Start is called before the first frame update
    void Start()
    {
        mainmenu.SetActive(true);
        options.SetActive(false);
    }

    private void Update()
    {
       
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
        
        loading.SetActive(true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MainScene");
    }
}
