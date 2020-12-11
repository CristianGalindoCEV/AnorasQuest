using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyMeele : MonoBehaviour
{

    //Player
    [SerializeField] private float speedChase = 5.5f;
    public float damage;
    private float m_speed = 6f;
    public EnemyHealth enemyhealth;
    public PlayerController playerController;
    public GameMaster gamemaster;
    private Collider m_collider;

    //Rango
    [SerializeField] float rangeDistanceMin;
    [SerializeField] float rangeDistanceMax;
    float rangeDistance = 6;
    [SerializeField] Transform player;
    public SpacePoint[] puntos;
    int currentPoint = 0;


    private void Awake()
    {
        rangeDistance = rangeDistanceMin;
        m_collider = this.GetComponent<CapsuleCollider>();
       
    }
    void Update()
    {
        
        //Miramos si hemos llegado al punto actual
        if(Vector3.Distance(transform.position, puntos[currentPoint].transform.position)< 0.2f){
            currentPoint++;
            currentPoint %= puntos.Length;
        }

        //Detecta Player
        if (Mathf.Abs(Vector3.Distance(player.position, transform.position)) < rangeDistance && enemyhealth.health > 0)
        {
            rangeDistance = rangeDistanceMax;    
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * speedChase);
        }

        //Patrulla siguiente punto
        else
        {
            rangeDistance = rangeDistanceMin;
            transform.position = Vector3.MoveTowards(transform.position, puntos[currentPoint].transform.position, Time.deltaTime * m_speed);
            
        }

        if (enemyhealth.health <= 0)
        {
            m_collider.enabled = false;
            m_speed = 0f;
        }
    }

    //Trigers
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword")
        {
            damage = gamemaster.swordDamage;
            enemyhealth.healtbarUI.SetActive(true);
            StartCoroutine(TakeDamage());
        }

        if (other.tag == "Bullet")
        {
            damage = gamemaster.bulletDamage;
            enemyhealth.healtbarUI.SetActive(true);
            StartCoroutine(TakeDamage());
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(Atack());
           // Debug.Log("attack");
        }
    }

    //Ataque
    IEnumerator Atack()
    {
        speedChase = 0f;
        playerController.Enemymele();
        //Animacion
        yield return new WaitForSeconds(2.0f);
        speedChase = 5.5f;
    }

    IEnumerator TakeDamage()
    {
        enemyhealth.health = enemyhealth.health - damage;
        yield return new WaitForSeconds(1.0f);
    }
}

