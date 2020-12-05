using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalItem : MonoBehaviour
{
    public PlayerController playercontroller;
    public GameMaster gameMaster;
    private GameObject loading;
    public Animator tranistion;
    public CanvasGroup hud;

    private void Start()
    {
        loading = GameObject.Find("loadingScreen");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameMaster.portal = true;

            StartCoroutine(Play());
        }
    }
    IEnumerator Play()
    {
        tranistion.SetBool("PressPlay", true);
        hud.alpha = 0;
        loading.SetActive(true);
        yield return new WaitForSeconds(4);
        SceneManager.LoadScene("StaticBoss");
    }
}