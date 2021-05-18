using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.VFX;
using Cinemachine;
public class FlyMiniBoss : MonoBehaviour
{
    private bool b_startFight = false;

    //Detected
    private Collider[] hit = new Collider[10];
    public LayerMask playerLayer;
    enum Patrol { MOVE, ROTATE }
    Patrol patrol = Patrol.MOVE;

    //SpacePoints
    public SpacePoint [] points;
    private int i_currentPoint = 0;

    //Boss
    public GameObject m_boss;
    public GameObject bossName;
    public GameObject insectPack;
    public GameObject ballAttack;
    public GameObject HP_Bar;
    public GameObject icon;
    private Animator m_anim;
    public CinemachineVirtualCamera deathcam;
    public CapsuleCollider m_collider;

    private bool b_figth = true;
    private bool b_stopColision = false;
    private float m_speed = 6;
    private int myRandom;

    //Player
    public Transform player;
    public PlayerStats playerStats;
    public Camera aimCam;
    public CinemachineFreeLook playerCam;
    public InputManager inputManager;

    //HP
    public MinibossHP minibosshp;
    public float damage;

    //Easing
    private float f_currentTime = 0;
   
    //Audio
    public AudioMixerSnapshot paused;
    private AudioSource flyAudio;

    [SerializeField] VisualEffect vfxSparkImpact;

    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animator>();
        flyAudio = GetComponent<AudioSource>();
        deathcam.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (b_startFight == true && minibosshp.hp > 0) 
        {
            f_currentTime += Time.deltaTime;
            
            switch(patrol)
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
                            transform.position = Vector3.MoveTowards(transform.position, points[i_currentPoint].transform.position, Time.deltaTime * m_speed);
                        }
                        break;
                    }
                case Patrol.ROTATE:
                    {
                        break;
                    }
            }

            // Loock Player
            if (minibosshp.hp > 0)
            {
                Vector3 loockAtPosition = player.position;
                loockAtPosition.x = transform.rotation.eulerAngles.y;
                transform.LookAt(player);
            }

            //Generate Attack
            if (f_currentTime >= 7f && minibosshp.hp > 0 && b_figth == true) 
            {
                myRandom = Random.Range(1, 3);
                switch (myRandom)
                {
                    case 1:
                    StartCoroutine(FirtsAttack());
                    break;

                    case 2:
                    StartCoroutine(SecondAttack());
                    break;
                    
                    default:
                    print("Falla el switch");
                    break;
                }
                b_figth = false;
            }
        }
    }
    private void FixedUpdate()
    {
        //Only use while dont figth
        if (!b_startFight && b_stopColision == false)
        {
            m_collider.enabled = false;
            hit = new Collider[10];
            //Create area for detect player
            Physics.OverlapSphereNonAlloc(transform.position, 30, hit, playerLayer);
            for (int i = 0; i < 10; i++)
            {
                if (hit[i] != null && hit[i].tag == "Player")
                {
                    // detecte el player
                    StartCoroutine(StartRound());
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            StartCoroutine(Damage());
            damage = playerStats.bulletDamage_stat; ;
        }
    }

    IEnumerator StartRound()
    {
        m_anim.SetBool("StartFigth", true);
        b_stopColision = true;
        yield return new WaitForSeconds(1.7f);
        flyAudio.Play();
        b_startFight = true;
        m_collider.enabled = true;
        m_anim.SetBool("Flying",true);
    }
    IEnumerator FirtsAttack() //This instance bullets
    {
        m_anim.SetBool("Attack", true);
        m_speed = 0;
        Vector3 bulletPosition = transform.position;
        bulletPosition.z = transform.position.z + 1;
        Instantiate(insectPack, transform.position, transform.rotation);
        FindObjectOfType<AudioManager>().Play("FlyBossAttack");
        
        yield return new WaitForSeconds(1.5f);
        m_speed = 6;
        m_anim.SetBool("Attack", false);
        f_currentTime = 0f;
        b_figth = true;
    }
    IEnumerator SecondAttack()
    {
        m_anim.SetBool("Attack", true);
        m_speed = 0;
        Vector3 bulletPosition = transform.position;
        bulletPosition.y = transform.position.y + 10;
        Instantiate(ballAttack, bulletPosition, transform.rotation);
        FindObjectOfType<AudioManager>().Play("FlyBossAttack");

        yield return new WaitForSeconds(2.5f);
        m_speed = 6;
        m_anim.SetBool("Attack", false);
        f_currentTime = 0f;
        b_figth = true;
    }
    IEnumerator StopMove()
    {
        float f_stop;
        f_stop = Random.Range(1f, 2.5f);
        m_speed = 0;
        yield return new WaitForSeconds(f_stop);
        m_speed = 6f;
    }
    IEnumerator Damage()
    {
        minibosshp.hp = minibosshp.hp - damage;
        vfxSparkImpact.SendEvent("SparkImpact");
        FindObjectOfType<AudioManager>().PlayRandomPitch("Impact");
        if (minibosshp.hp <= 0)
        {
           //Cameras
            deathcam.enabled = true;
            playerCam.enabled = false;
            aimCam.enabled = false;
            inputManager.animationPlayed = true;

            //HUD Disappear
            bossName.SetActive(false);
            minibosshp.bossBar.enabled = false;
            icon.SetActive(false);
            HP_Bar.SetActive(false);

            yield return new WaitForSeconds(3f);
            
            //Die animation + Shader
            m_anim.SetBool("DieMoth",true);
            b_startFight = false;
            m_collider.enabled = false;
            m_speed = 0;
            playerStats.FlyBoss = true;
            
            //AudioFade
            paused.TransitionTo(1.5f);
            
            yield return new WaitForSeconds(3.0f);
            playerCam.enabled = true;
            aimCam.enabled = true;
            deathcam.enabled = false;
            
            yield return new WaitForSeconds(1.5f);
            inputManager.animationPlayed = true;
            m_boss.SetActive(false);
        }
    }
}
