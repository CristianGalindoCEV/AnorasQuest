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

    //Rango
    public Collider Detection;
    private bool b_fight = false;
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
            f_time += Time.deltaTime;
            transform.LookAt(m_player);

            if(f_time >= 2f)
            {
                StartCoroutine(Attack());
            }
        }
    }

    //Trigers
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && b_fight == false)
        {
            b_fight = true;
            Detection.enabled = false;
        }

        if (other.tag == "Sword")
        {
            f_damage = gamemaster.swordDamage;
            enemyhealth.healtbarUI.SetActive(true);
            StartCoroutine(TakeDamage());
        }

        if (other.tag == "Bullet")
        {
            f_damage = gamemaster.bulletDamage;
            enemyhealth.healtbarUI.SetActive(true);
            StartCoroutine(TakeDamage());
        }
    }

    //Ataque
    IEnumerator Attack()
    {
        f_time = 0f;
        Instantiate(myBullet, transform.position, transform.rotation);
        //Animacion
        yield return new WaitForSeconds(1.0f);
    }

    IEnumerator TakeDamage()
    {
        enemyhealth.health = enemyhealth.health - f_damage;
        yield return new WaitForSeconds(0f);
    }
}

