using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTextAnimation : MonoBehaviour
{
    private MenuManager menuManager;
    // Start is called before the first frame update
    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            StartCoroutine(NeedButton());
        }
    }
    IEnumerator NeedButton()
    {
        menuManager.UiTextAnimation();
        menuManager.ControlText.SetText("You need to find a button");

        yield return new WaitForSeconds(3f);

        menuManager.ControlText.SetText("None");
        menuManager.Icon_L.enabled = false;
        menuManager.Icon_R.enabled = false;
        menuManager.Background_Icon.enabled = false;
    }
}
