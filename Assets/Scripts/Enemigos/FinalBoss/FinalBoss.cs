using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    //SpacePoints
    public SpacePoint[] points;
    private int i_currentPoint = 0;
    private bool b_move = true;

    //Boss
    private float f_speed = 8f;
    private float f_currentTime = 0;
    private bool b_onAttack = false;
    public GameObject m_boss;
    [SerializeField] private Collider m_collider;
    private bool b_startFight = false;

    //HP
    public MinibossHP minibosshp;
    public GameMaster gamemaster;
    public float damage;
    public GameObject bullets;

    //Player
    public Transform player;
    private Vector3 m_attackposition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (b_startFight == true)
        {
            f_currentTime += Time.deltaTime;
            if (b_onAttack == false)
            {
                if (Vector3.Distance(transform.position, points[i_currentPoint].transform.position) < 0.2f && b_move == true) //Miramos si hemos llegado al punto
                {
                    StartCoroutine(StopMove());
                    i_currentPoint++;
                    i_currentPoint %= points.Length;
                }
                else if (b_move == true) // Pasamos al siguiente punto
                {
                    transform.position = Vector3.MoveTowards(transform.position, points[i_currentPoint].transform.position, Time.deltaTime * f_speed);
                }
            }
            if (b_onAttack == true)
            {
                transform.LookAt(player);
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && b_startFight == false)
        {
            b_startFight = true;
            //StartCoroutine(StartRound());
        }
        if (other.tag == "Bullet")
        {
            StartCoroutine(Damage());
            damage = gamemaster.bulletDamage;
        }
        if (other.tag == "Sword")
        {
            StartCoroutine(Damage());
            damage = gamemaster.swordDamage;
        }
    }
    IEnumerator StopMove()
    {
        int myattack;
        b_onAttack = true;
        myattack = Random.Range(1,3);
        switch (myattack)
        {
            case 1:
                StartCoroutine(AttackOne());
                break;
            case 2:
                StartCoroutine(AttackTwo());
                break;
            default:
                Debug.Log("Falla switch");
                break;
        }
       yield return new WaitForSeconds(0);
    }
    IEnumerator AttackOne()
    {
        for (int i = 0; i<=4; i++)
        {
            Instantiate(bullets, transform.position, transform.rotation);
            yield return new WaitForSeconds(1f);
        }
        b_onAttack = false;
    }
    IEnumerator AttackTwo()
    {
        m_attackposition = player.position;
        for (int i = 0; i <= 4; i++)
        {
            Instantiate(bullets, transform.position, transform.rotation);
            yield return new WaitForSeconds(3f);
        }
        b_onAttack = false;
    }
    IEnumerator Damage()
    {
        minibosshp.hp = minibosshp.hp - damage;
        if (minibosshp.hp <= 0)
        {
            m_collider.enabled = false;
            f_speed = 0;
            yield return new WaitForSeconds(1.0f);
            m_boss.SetActive(false);
        }
    }
}
