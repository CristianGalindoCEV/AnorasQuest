using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class Bauculo : MonoBehaviour
{
    public Animator animator;
    public GameObject bullet;
    public float speed;
    private Transform firepoint;
    public PlayerController m_playerController;
    //Camera
    public CinemachineVirtualCamera aimCamera;

    void Start()
    {
        firepoint = GameObject.FindGameObjectWithTag("Hand").transform;
    }
    private void Update()
    {
        firepoint.rotation = aimCamera.transform.rotation;
    }
    public void Fire()
    {
        if (m_playerController.player.isGrounded == true) //Only Shot if u dont dont jump
        {
            StartCoroutine(Bullet());
        }  
    }
    IEnumerator Bullet()
    {
        animator.SetBool("PlayMeleAttack", true);
        
        yield return new WaitForSeconds(0.2f);
        Instantiate(bullet, firepoint.position, firepoint.rotation);
        FindObjectOfType<AudioManager>().PlayRandomPitch("MagicShot");
        
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("PlayMeleAttack", false);
    }
}
