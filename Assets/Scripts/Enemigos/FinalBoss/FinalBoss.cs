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
    private bool b_returnPoint = false; // bool para comprovar si ha llegado el boss a un punto despues de atacar
    private int i_myattack;
    private float f_randomTimeAttack;

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
        f_randomTimeAttack = Random.Range(3f,5f);
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
                    i_currentPoint++;
                    i_currentPoint %= points.Length;
                    b_returnPoint = false;
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

            if(f_currentTime >= f_randomTimeAttack && b_onAttack == false)
            {
                if (b_returnPoint == false)
                {
                    StartCoroutine(StopMove());
                } else
                    f_currentTime = 0;
            }
        }
        // Parte del attackTwo
        if (b_onAttack == true && i_myattack == 2) 
        {
            Vector3 playerPosition = player.transform.position;
            playerPosition.y = transform.position.y; 
            transform.LookAt(playerPosition);
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * 15);
            //Mirar si cuando reproduce una animacion se sigue moviendo
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && b_startFight == false)
        {
            b_startFight = true;
            //StartCoroutine(StartRound());

        }
        else if (other.tag == "Player" && b_startFight == true)
        {
            StartCoroutine(AttackTwo());
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
    IEnumerator StopMove() // Corutina para elegir un ataque y el tiempo para el proximo
    {
        i_myattack = Random.Range(1,3);
        b_onAttack = true;
        switch (i_myattack)
        {
            case 1:
                StartCoroutine(AttackOne());
                break;
            case 2:
                break;
            default:
                Debug.Log("Falla switch");
                break;
        }
        f_randomTimeAttack = Random.Range(3f, 5f);
        yield return new WaitForSeconds(0);
    }
    IEnumerator AttackOne()
    {
        for (int i = 0; i<=4; i++)
        {
            Instantiate(bullets, transform.position, transform.rotation);
            yield return new WaitForSeconds(1f);
        }
        f_currentTime = 0;
        b_onAttack = false;
    }
    IEnumerator AttackTwo()
    {
        b_returnPoint = true;
        //AnimacionAtaque
        yield return new WaitForSeconds(3f);
        f_currentTime = 0;
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
