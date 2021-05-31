using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class FadeInFadeOut : MonoBehaviour
{

    public Image whiteFade;
    private bool ahora = false;

    void Start()
    {
        whiteFade.canvasRenderer.SetAlpha(0.0f);

        fadeIn();

    }

    void fadeIn()
    {
        whiteFade.CrossFadeAlpha(1, 2, false);
        StartCoroutine(Go());
    }

    IEnumerator Go()
    {

        yield return new WaitForSeconds(3.0f);

        whiteFade.canvasRenderer.SetAlpha(1.0f);
        whiteFade.CrossFadeAlpha(0, 2, false);
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene("MainMenu");

    }

}