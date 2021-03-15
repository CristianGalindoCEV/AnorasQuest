using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossScript : MonoBehaviour
{
    //Boss

    [SerializeField] GameObject spikePrefab;
    [SerializeField] GameObject spikeCagePrefab;
    public Collider Mycollider;
    private Transform m_player;
    private Animator m_anim;
    private int randomNumber;
    public float damage;
    private float f_TimeCounter = 0;
    private bool b_startBattle = false;

    //Detect player
    private Collider[] hit = new Collider[10];
    public LayerMask playerLayer;

    //WallBoss
    private GameObject m_boss;
    public WallBoss wallBoss;
    private GameObject m_bossWall;
    
    //HP
    public MinibossHP minibosshp;
    public PlayerStats playerStats;

    void Start()
    {
        m_boss = GameObject.Find("Miniboss_Static");
        m_bossWall = GameObject.Find("Wall");
        m_anim = GetComponent<Animator>();
        m_player = GameObject.Find("Player").transform;
    }
    void Update()
    {
       if(b_startBattle == true)
        {
            
            f_TimeCounter += Time.deltaTime;

            //Ataques
            if (f_TimeCounter > 8 && minibosshp.hp > 0)
            {
                f_TimeCounter = 0;
                randomNumber = Random.Range(1, 3);

                switch (randomNumber)
                {
                    case 1:
                        StartCoroutine(SpikeAttack());
                        break;

                    case 2:
                        StartCoroutine(SpikeCage());
                        break;

                    default:
                        print("Falla el switch");
                        break;
                }
            }

            if (wallBoss.destroyWall == false && minibosshp.hp <= 1000)
            {
                minibosshp.hp = 1000;
                BossWall();
            }

            if (minibosshp.hp <= 0)
            {
                m_boss.SetActive(false);
            }
        }
    }

    private void FixedUpdate()
    {
        if (!b_startBattle)
        {
            hit = new Collider[10];
            //Create area for detect player
            Physics.OverlapSphereNonAlloc(transform.position, 10, hit, playerLayer);
            for (int i = 0; i < 10; i++)
            {
                if (hit[i] != null && hit[i].tag == "Player")
                {
                    // detecte el player
                    b_startBattle = true;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag  == "Bullet" && b_startBattle == true)
        {
            StartCoroutine(Damage());
            damage = playerStats.bulletDamage_stat;
        }
    }

    private void BossWall()
    {
        if (m_bossWall.transform.position.y <= -0.1f)
        {
            m_bossWall.transform.Translate(Vector3.up * Time.deltaTime);
        }
    }

    IEnumerator SpikeCage()
    {
        Instantiate(spikeCagePrefab, new Vector3(m_player.transform.position.x, 0, m_player.transform.position.z), transform.rotation);
        m_anim.SetBool("Attack", true);
        f_TimeCounter = 0;

        yield return new WaitForSeconds(1.5f);
        m_anim.SetBool("Attack", false);
    }
    IEnumerator SpikeAttack()
    {
        Instantiate(spikePrefab, new Vector3(m_player.transform.position.x, 0, m_player.transform.position.z), transform.rotation);
        m_anim.SetBool("Attack", true);

        f_TimeCounter = 0;

        yield return new WaitForSeconds(1.5f);
        m_anim.SetBool("Attack", false);
    }
    IEnumerator Damage()
    {
        minibosshp.hp = minibosshp.hp - damage;
        if (minibosshp.hp <= 0)
        {
            b_startBattle = false;
            m_anim.SetBool("Dead",true);
            Mycollider.enabled = false;
            yield return new WaitForSeconds(1.0f);
            m_boss.SetActive(false);
        }
    }

}
