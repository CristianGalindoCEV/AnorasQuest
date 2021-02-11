using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossScript : MonoBehaviour
{
    //Boss
    private float f_TimeCounter = 0;
    [SerializeField] GameObject spikePrefab;
    [SerializeField] GameObject spikeCagePrefab;
    [SerializeField] Transform player;
    private int randomNumber;
    public float damage;
    private bool b_startBatle = false;
    public Collider myStart;
    
    //WallBoss
    private GameObject m_boss;
    public WallBoss wallBoss;
    private GameObject m_bossWall;
    
    //HP
    public MinibossHP minibosshp;
    public GameMaster gamemaster;

    void Start()
    {
        m_boss = GameObject.Find("Miniboss_Static");
        m_bossWall = GameObject.Find("Wall");
    }
    void Update()
    {
       if(b_startBatle == true)
        {
            f_TimeCounter += Time.deltaTime;

            //Ataques
            if (f_TimeCounter > 3 && minibosshp.hp > 0)
            {
                f_TimeCounter = 0;
                randomNumber = Random.Range(1, 10);

                if (randomNumber < 6)
                {
                    SpikeAttack();
                }
                else
                {
                    SpikeCage();
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && b_startBatle == false)
        {
            b_startBatle = true;
            myStart.enabled = false;
        }
        if ( other.tag  == "Bullet" && b_startBatle == true)
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

    private void BossWall()
    {
        if (m_bossWall.transform.position.y <= -0.1f)
        {
            m_bossWall.transform.Translate(Vector3.up * Time.deltaTime);
        }
    }

    void SpikeAttack ()
    {
        Instantiate(spikePrefab, new Vector3(player.transform.position.x, 0, player.transform.position.z), transform.rotation);
    }

    void SpikeCage ()
    {
        Instantiate(spikeCagePrefab, new Vector3(player.transform.position.x, 0, player.transform.position.z), transform.rotation);
    }

    IEnumerator Damage()
    {
        minibosshp.hp = minibosshp.hp - damage;
        yield return new WaitForSeconds(1.0f);
    }

}
