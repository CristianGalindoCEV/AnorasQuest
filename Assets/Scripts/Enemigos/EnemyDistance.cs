using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistance : MonoBehaviour
{

    //Player
    private float f_damage;
    public PlayerController playerController;
    private Transform m_player;
    private float f_speed = 6;

    //SpacePoints
    public SpacePoint[] points;
    private int i_currentPoint = 0;

    //Enemy
    public PlayerStats playerStats;
    public EnemyHealth enemyhealth;
    private Collider m_enemyCollider;
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
        m_enemyCollider = gameObject.GetComponent<Collider>();
    }
    void Update()
    {
        if (!b_fight)
        {
            if (Vector3.Distance(transform.position, points[i_currentPoint].transform.position) < 0.5f)
            {
                StartCoroutine(StopMove());
                i_currentPoint++;
                i_currentPoint %= points.Length;
            }
            else // Pasamos al siguiente punto
            {
                transform.position = Vector3.MoveTowards(transform.position, points[i_currentPoint].transform.position, Time.deltaTime * f_speed);
                transform.LookAt(points[i_currentPoint].transform.position);
            }
        }
        
        if (b_fight == true && enemyhealth.health > 0)
        {   
            float dist = Vector3.Distance(transform.position, m_player.transform.position);
            if (dist > 15f)
            {
                b_fight = false;
            }

            f_time += Time.deltaTime;
            Vector3 loockAtPosition = m_player.position;
            loockAtPosition.y = transform.position.y;
            transform.LookAt(loockAtPosition);
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
            f_damage = playerStats.bulletDamage_stat;
            enemyhealth.healtbarUI.SetActive(true);
            StartCoroutine(TakeDamage());
        }
    }
    //Ataque
    IEnumerator Attacks()
    {
        f_time = 0f;
        Instantiate(myBullet, transform.position, transform.rotation);
        //Animacion
        yield return new WaitForSeconds(0f);
    }
    IEnumerator TakeDamage()
    {
        enemyhealth.health = enemyhealth.health - f_damage;
        if (enemyhealth.health <= 0)
        {
            m_enemyCollider.enabled = false;
            yield return new WaitForSeconds(3f);
        }
        yield return new WaitForSeconds(0f);
    }
    IEnumerator StopMove()
    {
        float f_stop;
        //transform.rotation = Quaternion.Slerp(transform.rotation, (points[i_currentPoint].transform.rotation), Time.time * 1f);
        f_stop = Random.Range(1f, 2.5f);
        f_speed = 0;
        //Idle Aimation
        yield return new WaitForSeconds(f_stop);
        f_speed = 6f;
    }
}

