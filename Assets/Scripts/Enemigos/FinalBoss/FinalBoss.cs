using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    //SpacePoints
    public SpacePoint[] points;
    private int i_currentPoint = 0;
    enum Patrol { MOVE, ROTATE }
    Patrol patrol = Patrol.MOVE;
   
    //Detected
    private Collider[] hit = new Collider[10];
    public LayerMask playerLayer;

    //Boss
    public GameObject bullets;
    public GameObject Enemyes;

    public float speed = 8f;
    private float f_currentTime = 0;
    private bool b_onAttack = false;
    public GameObject m_boss;
    [SerializeField] private Collider m_collider;
    private bool b_startFight = false;
    private int i_myattack;
    private float f_randomTimeAttack;

    //HP
    public MinibossHP minibosshp;
    public PlayerStats playerStats;
    public float damage;

    //Player
    public Transform player;

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
                switch (patrol)
                {
                    case Patrol.MOVE:
                        {
                             //Miramos si hemos llegado al punto
                            if (Vector3.Distance(transform.position, points[i_currentPoint].transform.position) < 0.2f && minibosshp.hp > 0)
                            {
                                //patrol = Patrol.ROTATE;
                                i_currentPoint++;
                                i_currentPoint %= points.Length;
                                StartCoroutine(StopMove());
                            }
                            else // Pasamos al siguiente punto
                            {
                            transform.position = Vector3.MoveTowards(transform.position, points[i_currentPoint].transform.position, Time.deltaTime * speed);
                            }
                           break;
                            }
                    case Patrol.ROTATE:
                        {
                            break;
                        }
                }
            } 
        }

        // Loock Player
        if (minibosshp.hp > 0)
        {
            Vector3 loockAtPosition = player.position;
            loockAtPosition.y = transform.position.y;

            transform.LookAt(loockAtPosition);
            
        }
        // Create attack
        if (f_currentTime >= 7f && minibosshp.hp > 0 && b_onAttack == false)
        {
            i_myattack = Random.Range(1, 3);
            switch (i_myattack)
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
            f_randomTimeAttack = Random.Range(3f, 5f);
            b_onAttack = true;
        }
    }
    private void FixedUpdate()
    {
        if (!b_startFight)
        {
            hit = new Collider[10];
            //Create area for detect player
            Physics.OverlapSphereNonAlloc(transform.position, 30, hit, playerLayer);
            for (int i = 0; i < 10; i++)
            {
                if (hit[i] != null && hit[i].tag == "Player")
                {
                    // detecte el player
                    b_startFight = true;
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" && b_startFight == true)
        {
            StartCoroutine(Damage());
            damage = playerStats.bulletDamage_stat;
        }
    }
    IEnumerator StopMove()
    {
        float f_stop;
        f_stop = Random.Range(1f, 2.5f);
        speed = 0;
        yield return new WaitForSeconds(f_stop);
        speed = 8;
    }
    IEnumerator AttackOne()
    {
        for (int i = 0; i<=4; i++)
        {
            Instantiate(bullets, transform.position, transform.rotation);
            FindObjectOfType<AudioManager>().Play("BossShot");
            transform.LookAt(player);
            yield return new WaitForSeconds(1f);
        }
        f_currentTime = 0;
        b_onAttack = false;
    }
    IEnumerator AttackTwo()
    {
        Vector3 newPosition;
        float randomNumber;

        for (int i = 0; i <= 4; i++)
        {
            randomNumber = Random.Range (10,25);
            newPosition = new Vector3(player.position.x + randomNumber, player.position.y + randomNumber, player.position.z + randomNumber);
            Instantiate(Enemyes, transform.position, transform.rotation);
            
            //Audio enemigo instancia
            yield return new WaitForSeconds(2f);
        }
        f_currentTime = 0;
        b_onAttack = false;
    }
    IEnumerator Damage()
    {
        minibosshp.hp = minibosshp.hp - damage;
        if (minibosshp.hp <= 0)
        {
            m_collider.enabled = false;
            speed = 0;
            yield return new WaitForSeconds(1.0f);
            m_boss.SetActive(false);
        }
    }
}
