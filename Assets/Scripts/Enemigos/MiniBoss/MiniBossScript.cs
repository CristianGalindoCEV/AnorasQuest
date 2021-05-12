using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.VFX;

public class MiniBossScript : MonoBehaviour
{
    //Boss
    [SerializeField] GameObject spikePrefab;
    public Collider MycolliderTrigger;
    public Collider MycolliderNoTrigger;
    private Animator m_anim;
    public GameObject bossName;

    private int randomNumber;
    public float damage;
    private float f_TimeCounter = 0;
    private bool b_startBattle = false;

    //Player
    private Transform m_player;
    private Collider[] hit = new Collider[10];
    public LayerMask playerLayer;

    //WallBoss
    private GameObject m_boss;
    public WallBoss wallBoss;
    public GameObject bossWall;
    
    //HP
    public MinibossHP minibosshp;
    public PlayerStats playerStats;
   
    //Audio
    public AudioMixerSnapshot paused;

    private VisualEffect vfxSparkImpact;

    void Start()
    {
        m_boss = GameObject.Find("Miniboss_Static");
        m_anim = GetComponent<Animator>();
        m_player = GameObject.Find("Player").transform;
        bossName.SetActive(true);
        vfxSparkImpact = GetComponent<VisualEffect>();
    }
    void Update()
    {
       if(b_startBattle == true)
        {
            
            f_TimeCounter += Time.deltaTime;

            //Ataques
            if (f_TimeCounter > 6 && minibosshp.hp > 0)
            {
                f_TimeCounter = 0;
                randomNumber = Random.Range(1, 3);

                switch (randomNumber)
                {
                    case 1:
                        StartCoroutine(SpikeAttack());
                        break;

                    case 2:
                        StartCoroutine(SpikeAttack());
                        break;

                    default:
                        print("Falla el switch");
                        break;
                }
            }

            if (wallBoss.destroyWall == false && minibosshp.hp <= 600)
            {
                minibosshp.hp = 600;
                BossWall();
            }
        }
        
       if (Input.GetKeyDown(KeyCode.C))
        {
            Damage();
        }
    }

    private void FixedUpdate()
    {
        if (!b_startBattle)
        {
            hit = new Collider[10];
            //Create area for detect player
            Physics.OverlapSphereNonAlloc(transform.position, 40, hit, playerLayer);
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
        if (bossWall.transform.position.y <= -0.1f)
        {
            bossWall.transform.Translate(Vector3.up * Time.deltaTime);
        }
    }
    IEnumerator SpikeAttack()
    {
        Vector3 myPlayerPosition = m_player.transform.position;
        yield return new WaitForSeconds(1f);
        Instantiate(spikePrefab, new Vector3(myPlayerPosition.x, 0, myPlayerPosition.z), transform.rotation);
        m_anim.SetBool("Attack", true);
        
        f_TimeCounter = 0;

        yield return new WaitForSeconds(1.5f);
        m_anim.SetBool("Attack", false);
    }
    IEnumerator Damage()
    {
        minibosshp.hp = minibosshp.hp - damage;
        vfxSparkImpact.SendEvent("SparkImpact");
        FindObjectOfType<AudioManager>().PlayRandomPitch("Impact");
        if (minibosshp.hp <= 0)
        {
            //Die animation + Shader
            b_startBattle = false;
            m_anim.SetBool("Dead",true);
            MycolliderTrigger.enabled = false;
            MycolliderNoTrigger.enabled = false;
            playerStats.StaticBoss = true;
            
            //HUD Disappear
            bossName.SetActive(false);
            minibosshp.bossBar.enabled = false;
            
            //AudioFade
            paused.TransitionTo(1.5f);

            yield return new WaitForSeconds(10.0f);
            m_boss.SetActive(false);
        }
    }

}
