using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class SpawnPointBoses : MonoBehaviour
{
    //Animation
    public Camera aimCamera;
    public CinemachineFreeLook playerCamera;
    public PlayableDirector director;
    public PlayerController playerController;
    public InputManager inputManager;

    //Position
    public PlayerStats playerStats;
    public Vector3 position;
   
    // Start is called before the first frame update
    void Start()
    {
        if (playerStats.FlyBoss == false && playerStats.StaticBoss == false) // If u don't kill any boss
        {
            playerStats.playerPosition_stat = position;
        }
        else
        {
            position = playerStats.playerPosition_stat;
        }
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
        playerController.speed = 10f;
        inputManager.animationPlayed = false;
    }
}
