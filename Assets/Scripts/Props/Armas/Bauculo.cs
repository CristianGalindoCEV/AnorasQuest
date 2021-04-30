using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bauculo : MonoBehaviour
{
    public Animator animator;
    public GameObject bullet;
    public float Bulletspeed = 20f;
    [SerializeField]
    private Transform firepoint;
    [SerializeField]
    private Camera aimCamera;
    public PlayerController m_playerController;
    Vector3 lookAt;
    public LayerMask layerMask;

    void Start()
    {
        //firepoint = GameObject.FindGameObjectWithTag("Hand").transform;
    }
    private void Update()
    {
        firepoint.rotation = m_playerController.aimCamera.transform.rotation;
    }
    public void Fire()
    {
        if (m_playerController.player.isGrounded == true) //Only Shot if u dont dont jump
        {
            Ray ray = aimCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                lookAt = hit.point;

                StartCoroutine(Bullet());
                //Debug.Log(hit.transform.name);
            }

        }  
    }
    IEnumerator Bullet()
    {
        animator.SetBool("PlayMeleAttack", true);
        
        yield return new WaitForSeconds(0.2f);
        Instantiate(bullet, firepoint.position, Quaternion.LookRotation(lookAt - firepoint.position));
        FindObjectOfType<AudioManager>().PlayRandomPitch("MagicShot");
        
        yield return new WaitForSeconds(0.4f);
        animator.SetBool("PlayMeleAttack", false);
    }
}
