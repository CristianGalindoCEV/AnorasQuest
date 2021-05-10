using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTextAnimation : MonoBehaviour
{
    private MenuManager menuManager;
    private bool b_AnimationPlaying = false;
    // Start is called before the first frame update
    void Start()
    {
        menuManager = FindObjectOfType<MenuManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && b_AnimationPlaying == false)
        {
            StartCoroutine(NeedButton());
        }
    }
    IEnumerator NeedButton()
    {
        menuManager.UiTextAnimation();
        b_AnimationPlaying = true;
        menuManager.ControlText.SetText("You need to find a button");

        yield return new WaitForSeconds(5f);

        b_AnimationPlaying = false;
        menuManager.ControlText.SetText("None");
        menuManager.ControlText.enabled = false;
        menuManager.Icon_L.enabled = false;
        menuManager.Icon_R.enabled = false;
        menuManager.Background_Icon.enabled = false;
    }
}
