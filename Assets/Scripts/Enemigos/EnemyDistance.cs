using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistance : MonoBehaviour
{

    //Player
    private float f_damage;
    public PlayerController playerController;
    private Transform m_player;

    //Enemy
    public GameMaster gamemaster;
    public EnemyHealth enemyhealth;
    [SerializeField] private GameObject myBullet;
    private Collider[] hit = new Collider[10];
    public LayerMask playerLayer;

    //Rango
    [SerializeField] private bool b_fight = false;
    private float f_time;

    //Idle
    [SerializeField] private float f_stop;

    private void Awake()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if (b_fight == true)
        {   
            float dist = Vector3.Distance(transform.position, m_player.transform.position);
            if (dist > 15f)
            {
                b_fight = false;
            }

            f_time += Time.deltaTime;
            transform.LookAt(m_player);
            if (f_time >= 3f)
            {
                StartCoroutine(Attacks());
            }
        }
    }

    private void FixedUpdate()
    {
        if (!b_fight)
        {
            hit = new Collider[10];
            Physics.OverlapSphereNonAlloc(transform.position, 10, hit, playerLayer);
            for (int i = 0; i < 10; i++)
            {
                if (hit[i] != null && hit[i].tag == "Player")
                {
                    // detecte el player
                    b_fight = true;
                }
            }
        }
    }

    //Trigers
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            f_damage = gamemaster.bulletDamage;
            enemyhealth.healtbarUI.SetActive(true);
            StartCoroutine(TakeDamage());
        }
    }

    //Ataque
    IEnumerator Attacks()
    {
        Debug.Log("A");
        f_time = 0f;
        Instantiate(myBullet, transform.position, transform.rotation);
        //Animacion
        yield return new WaitForSeconds(0f);
    }
    IEnumerator TakeDamage()
    {
        enemyhealth.health = enemyhealth.health - f_damage;
        yield return new WaitForSeconds(0f);
    }
}

