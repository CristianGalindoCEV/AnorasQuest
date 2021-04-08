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
        Instantiate(bullet, firepoint.position, firepoint.rotation);
        
        /*Debug.DrawRay(firepoint.position, firepoint.forward * 100, Color.red, 2f);
        Ray ray = new Ray(firepoint.position, firepoint.forward);
        RaycastHit hitInfo;*/
        
        FindObjectOfType<AudioManager>().PlayRandomPitch("MagicShot");
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("PlayMeleAttack", false);
    }
}
