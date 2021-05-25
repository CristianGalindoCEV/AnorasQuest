using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class SpawnPointBoses : MonoBehaviour
{
    //Animation
    public Camera aimCamera;
    public CinemachineVirtualCamera animationCam;
    public CinemachineFreeLook playerCamera;
    public PlayableDirector director;
    public PlayerController playerController;
    public InputManager inputManager;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Animation());
    }

    IEnumerator Animation()
    {
        playerController.speed = 0f;
        director.Play();
        aimCamera.enabled = false;
        playerCamera.enabled = false;
        inputManager.animationPlayed = true;
        
        yield return new WaitForSeconds(4f);
        director.Stop();
        aimCamera.enabled = true;
        playerCamera.enabled = true;
        yield return new WaitForSeconds(1f);
        animationCam.enabled = false;
        playerController.speed = 10f;
        inputManager.animationPlayed = false;
    }
}
