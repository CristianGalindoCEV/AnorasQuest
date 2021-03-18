using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyDistance : MonoBehaviour
{
    enum Patrol { MOVE, ROTATE}
    Patrol patrol = Patrol.MOVE;
    
    //Player
    private float f_damage;
    private Transform m_player;
    private float f_speed = 6;

    //SpacePoints
    public SpacePoint[] points;
    private int i_currentPoint = 0;
    private Vector3 m_direction;
    private Quaternion m_lookrotation;

    //Enemy
    private Animator m_animator;
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
        m_animator = this.GetComponent<Animator>();
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        m_enemyCollider = gameObject.GetComponent<Collider>();
    }
    void Update()
    {
        enemyhealth.slider.transform.LookAt(m_player);
        if (!b_fight)
        {
            switch(patrol)
            {
                case Patrol.MOVE:
                    {
                        if (Vector3.Distance(transform.position, points[i_currentPoint].transform.position) < 0.5f)
                        {
                            patrol = Patrol.ROTATE;
                            i_currentPoint++;
                            i_currentPoint %= points.Length;
                            StartCoroutine(StopMove());
                            m_direction = (points[i_currentPoint].transform.position - transform.position).normalized;

                            //create the rotation we need to be in to look at the target
                            m_lookrotation = Quaternion.LookRotation(m_direction);
                            transform.DORotate( m_lookrotation.eulerAngles, 1f).SetEase(Ease.InSine);
                        }
                        else // Pasamos al siguiente punto
                        {
                            transform.position = Vector3.MoveTowards(transform.position, points[i_currentPoint].transform.position, Time.deltaTime * f_speed);
                            transform.LookAt(points[i_currentPoint].transform.position);
                        }
                        break;
                    }
                case Patrol.ROTATE:
                    {
                        break;
                    }
            }
        }
        
        if (b_fight == true && enemyhealth.health > 0)
        {   
            float dist = Vector3.Distance(transform.position, m_player.transform.position);
            if (dist > 15f)
            {
                b_fight = false; //Player attack range? No
            }

            f_time += Time.deltaTime;
            Vector3 loockAtPosition = m_player.position;
            loockAtPosition.y = transform.position.y;
            transform.LookAt(loockAtPosition);
            if (f_time >= 1.5f)
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
            //Create area for detect player
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
        m_animator.SetBool("Attack",true);
        yield return new WaitForSeconds(0.5f);
        m_animator.SetBool("Attack", false);
    }
    IEnumerator TakeDamage()
    {
        enemyhealth.health = enemyhealth.health - f_damage;
        if (enemyhealth.health <= 0)
        {
            m_animator.SetBool("Death",true);
            m_enemyCollider.enabled = false;
        }
        yield return new WaitForSeconds(0f);
    }
    IEnumerator StopMove()
    {
        float f_stop;
        
        f_speed = 0;
        m_animator.SetBool("Stop", true);
        f_stop = Random.Range(1.5f, 2.5f);
        yield return new WaitForSeconds(f_stop);
        m_animator.SetBool("Stop", false);
        patrol = Patrol.MOVE;
        f_speed = 6f;
    }
}

