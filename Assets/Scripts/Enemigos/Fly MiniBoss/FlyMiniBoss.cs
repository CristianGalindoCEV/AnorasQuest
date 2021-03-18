using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMiniBoss : MonoBehaviour
{
    
    private bool b_startFight = false;
    
    //SpacePoints
    public SpacePoint [] points;
    private int i_currentPoint = 0;
    private bool b_move = true; // Para saber cuando el enemy se mueve

    //Boss
    public GameObject m_boss;
    public GameObject insectPack;
    [SerializeField] private GameObject m_neck;
    Transform my_transform;
    private Animator m_anim;

    public Collider m_triger;
    private Collider m_collider;
    
    private float speed = 6;
    private int myRandom;
    private bool b_returnAttack = true; //Cuando el enemy ha vuelto a su punto depsues de atacar

    //Player
    public Transform player;
    private Vector3 m_attackposition;
    public PlayerStats playerStats;

    //HP
    public MinibossHP minibosshp;
    public float damage;

    //Easing
    private float f_currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        my_transform = transform;
        m_neck = GameObject.Find("J_Neck");
        m_anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (b_startFight == true) 
        {
            f_currentTime += Time.deltaTime;

            // Loock Player
            if (b_move == true && minibosshp.hp > 0)
            {
                Vector3 loockAtPosition = player.position;
                loockAtPosition.x = transform.rotation.eulerAngles.y;
                transform.LookAt(player);
            }
            
            //Miramos si hemos llegado al punto
            if (Vector3.Distance(transform.position, points[i_currentPoint].transform.position) < 0.2f && b_move == true && minibosshp.hp > 0) 
            {
                StartCoroutine(StopMove());
                i_currentPoint++;
                i_currentPoint %= points.Length;
            }
            else if (b_move == true) // Pasamos al siguiente punto
            {
                transform.position = Vector3.MoveTowards(transform.position, points[i_currentPoint].transform.position, Time.deltaTime * speed);
            }
            
            //Generate Attack
            if (f_currentTime >= 7f && b_returnAttack == true && minibosshp.hp > 0) 
            {
                myRandom = Random.Range(1, 3);
                switch (myRandom)
                {
                    case 1:
                    StartCoroutine(FirtsAttack());
                    break;

                    case 2:
                    StartCoroutine(FirtsAttack());
                    break;
                    
                    default:
                    print("Falla el switch");
                    break;
                }
            }
            
            // Move attack 2
            if (b_move == false && myRandom == 2)
            {
                transform.position = Vector3.MoveTowards(transform.position,m_attackposition, Time.deltaTime * 15);
            }
        }
    }

    private void LateUpdate()
    {
        if (b_startFight == true)
        {
            //Neck Rotation

            Quaternion lookRotation = Quaternion.LookRotation(player.position - m_neck.transform.position);
            m_neck.transform.rotation *= lookRotation;
            //m_neck.transform.eulerAngles.y = Mathf.Clamp()
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && b_startFight == false)
        {
            StartCoroutine(StartRound());           
        }
        if (other.tag == "Bullet")
        {
            StartCoroutine(Damage());
            damage = playerStats.bulletDamage_stat; ;
        }
    }

    IEnumerator StartRound()
    {
        m_anim.SetBool("StartFigth", true);
        m_triger.enabled = false;
        //Meter Audio
        
        yield return new WaitForSeconds(1.7f);
        b_startFight = true;
        m_anim.SetBool("Flying",true);
    }
    IEnumerator FirtsAttack() //This instance bullets
    {
        m_anim.SetBool("Attack", true);
        Vector3 bulletPosition = transform.position;
        bulletPosition.z = transform.position.z + 1;
        Instantiate(insectPack, transform.position, transform.rotation);
        FindObjectOfType<AudioManager>().Play("FlyBossAttack");
        b_returnAttack = false;
        
        yield return new WaitForSeconds(1.5f);
        
        m_anim.SetBool("Attack", false);
        f_currentTime = 0f;
        b_move = true;
        b_returnAttack = true;
    }
    IEnumerator SecondAttack()
    {
        Debug.Log("SecondAttack");
        Vector3 attackposition = player.position;
        b_move = false;
        b_returnAttack = false;
        yield return new WaitForSeconds(5f);
        f_currentTime = 0f;
        b_move = true;
        yield return new WaitForSeconds(12f); // Enemy return to the point
        b_returnAttack = true;
    }
    IEnumerator StopMove()
    {
        float f_stop;
        f_stop = Random.Range(1f, 2.5f);
        speed = 0;
        yield return new WaitForSeconds(f_stop);
        speed = 6f;
    }
    IEnumerator Damage()
    {
        minibosshp.hp = minibosshp.hp - damage;
        if(minibosshp.hp <= 0)
        {
            m_anim.SetBool("DieMoth",true);
            b_startFight = false;
            m_collider.enabled = false;
            speed = 0;
            yield return new WaitForSeconds(1.0f);
            m_boss.SetActive(false);
        }
    }
}
