using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EnemyFinalBossInstance : MonoBehaviour
{  
    //Player
    private float f_damage;
    private Transform m_player;

    //Enemy
    private MinibossHP m_minibosshp;
    private Animator m_animator;
    public PlayerStats playerStats;
    public EnemyHealth enemyhealth;
    private Collider m_enemyCollider;
    [SerializeField] private GameObject myBullet;
    
    //Rango
    [SerializeField] private bool b_fight = false;
    private float f_time;

    //Idle
    [SerializeField] private float f_stop;

    private void Awake()
    {
        m_minibosshp = FindObjectOfType<MinibossHP>();
        m_animator = this.GetComponent<Animator>();
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        m_enemyCollider = gameObject.GetComponent<Collider>();
    }
    void Update()
    {
        enemyhealth.slider.transform.LookAt(m_player);
       
        f_time += Time.deltaTime;
        Vector3 loockAtPosition = m_player.position;
        loockAtPosition.y = transform.position.y;
        transform.LookAt(loockAtPosition);
            
        if (f_time >= 3f && enemyhealth.health > 0 && m_minibosshp.hp > 0)
        {
            StartCoroutine(Attacks());
        }
        if (m_minibosshp.hp < 0)
        {
            Destroy(this.gameObject);
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
}

