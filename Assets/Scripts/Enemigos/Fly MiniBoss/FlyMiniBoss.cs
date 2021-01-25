using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMiniBoss : MonoBehaviour
{
    
    private bool b_startFight = false;
    
    //SpacePoints
    public SpacePoint [] points;
    private int i_currentPoint = 0;
    private bool b_move = true;

    //Boss
    [SerializeField] private float f_speed = 3;
    Transform my_transform;
    public GameObject insectPack;
    public Collider m_triger;
    private Vector3[] totalPoints;
    [SerializeField] private Collider m_collider;

    //Player
    public Transform player;
    private Vector3 m_attackposition;

    //HP
    public MinibossHP minibosshp;
    public GameMaster gamemaster;
    public float damage;

    //Easing
    [SerializeField]private float f_currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        my_transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (b_startFight == true) 
        {
            f_currentTime += Time.deltaTime;

            //Hay que retocarlo
            if (b_move == true)
            {
                Vector3 loockAtPosition = player.position;
                loockAtPosition.x = transform.rotation.eulerAngles.y;
                transform.LookAt(player);
            }

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
            
            if (f_currentTime > 7f && b_move == true)
            {
                int i = Random.Range(1, 3);

                switch (i)
                {
                    case 1:
                    StartCoroutine(SecondAttack());
                    break;

                    case 2:
                    StartCoroutine(SecondAttack());
                    break;
                    
                    default:
                    print("Falla el switch");
                    break;
                }
            }
            if (b_move == false)
            {
                transform.position = Vector3.MoveTowards(transform.position,m_attackposition, Time.deltaTime * 15);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && b_startFight == false)
        {
            b_startFight = true;
            StartCoroutine(StartRound());           
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

    IEnumerator StartRound()
    {
        float initValue = my_transform.position.y;
        float finalValue = my_transform.position.y + 10;
        float currentValue = initValue;
        float maxTime = 5f;

        m_triger.enabled = false;
        currentValue = Easing.CubicEaseIn(f_currentTime, initValue, finalValue - initValue, maxTime);
        my_transform.position = new Vector3(my_transform.position.x,currentValue,my_transform.position.z);

        //Meter Audio
        Debug.Log("Empieza la pelea");
        yield return new WaitForSeconds(7f);
    }
    IEnumerator FirtsAttack()
    {
        Debug.Log("FirtsAttack");
        Vector3 bulletPosition = transform.position;
        bulletPosition.z = transform.position.z + 1;
        Instantiate(insectPack, transform.position, transform.rotation);

        yield return new WaitForSeconds(0f);
        f_currentTime = 0f;
        b_move = true;
    }
    IEnumerator SecondAttack()
    {
        Debug.Log("SecondAttack");
        Vector3 attackposition = player.position;
        b_move = false;
        yield return new WaitForSeconds(7f);
        f_currentTime = 0f;
        b_move = true;
    }
    IEnumerator StopMove()
    {
        float f_stop;
        
        f_stop = Random.Range(1f, 2.5f);
        f_speed = 0;
        
        //Idle Aimation
        yield return new WaitForSeconds(f_stop);
        f_speed = 6f;
    }
    IEnumerator Damage()
    {
        minibosshp.hp = minibosshp.hp - damage;
        if(minibosshp.hp <= 0)
        {
            m_collider.enabled = false;
        }
        yield return new WaitForSeconds(1.0f);
    }
}
