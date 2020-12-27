using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : PhysicsCollision
{
   
    //Player
    private Rigidbody m_rigidbody;
    private float m_horizontalMove;
    private float m_verticalMove;
    private Vector3 m_playerInput;
    private Transform m_transform;
    private float damage;
    private float heal;
    [SerializeField] private float m_playerspeed = 5;
    private Vector3 m_movePlayer;
    [SerializeField] private bool iamdead = false;
    public PhysicsCollision pysicsCollision;
    public bool god = false;
    public Animator transtion;
    [SerializeField] private float f_cadence;
    private float f_cadenceTime = 0;

    //Dash & sprint
    [SerializeField] private float f_dashSpeed;
    [SerializeField] private float f_dashDuration;
    [SerializeField] private float f_sprint;
    public bool sprinting = false;

    //Camera
    [SerializeField] private Transform m_cameraTransform;
    [SerializeField]
    private Vector3 camForward;
    private Vector3 camRight;

    //Jump & Gravity
    [SerializeField] private float f_jumpForce = 0.5f;
    [SerializeField] private CapsuleCollider m_playerCol;

    //Canvas
    public GameObject healthbar;
    public GameMaster gamemaster;
    public GameObject stamina;
    public StaminaBar staminabar;

    //Shadow
    [SerializeField] GameObject m_shadowGO;
    [SerializeField] Transform m_shadowTransform;
    [SerializeField] LayerMask m_groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_transform = transform;
        m_playerCol = GetComponent<CapsuleCollider>();

    }

    private void Update()
    {
        m_horizontalMove = Input.GetAxis("Horizontal");
        m_verticalMove = Input.GetAxis("Vertical");

        m_playerInput = new Vector3(m_horizontalMove, 0, m_verticalMove);
        m_playerInput = Vector3.ClampMagnitude(m_playerInput, 1);

        camDirection();

        m_movePlayer = m_playerInput.x * camRight + m_playerInput.z * camForward;

        m_movePlayer = m_movePlayer * m_playerspeed;

        m_transform.LookAt(m_transform.position + m_movePlayer);
        
        if (!god)
            m_movePlayer.y = m_rigidbody.velocity.y;

        f_cadenceTime += Time.deltaTime;

       
        //Run
        if (sprinting == false && god == false)
        {
            m_playerspeed = 5;
        }

        // GOOD MODE
        if (god == true)
        {
            if (Input.GetKey(KeyCode.F1))
            {
                SceneManager.LoadScene("StaticBoss");
            }
            
            if (Input.GetKey(KeyCode.F2))
            {
                SceneManager.LoadScene("MainScene");
            }

            if (Input.GetKey(KeyCode.M))
                m_movePlayer.y += 20;

            if (Input.GetKey(KeyCode.N))
                m_movePlayer.y -= 20;

            while ( gamemaster.hp < gamemaster.maxhp)
            {
                gamemaster.hp++;
            }

        }

        m_rigidbody.velocity = m_movePlayer;

        m_rigidbody.useGravity = !god;

    }

    private void LateUpdate()
    {
        if (!isGrounded)
        {
            m_shadowGO.SetActive(true);
            RaycastGround();

        }
        else
        {
            m_shadowGO.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Wall" && isGrounded == false) // esta bien
        {
            m_movePlayer.x = 0;
            Vector3 velocity = m_rigidbody.velocity;
            velocity.x = 0;
            //velocity.y = -100; te empuja para abajo pero queda raro
            m_rigidbody.velocity = velocity;
            m_playerspeed = 0;
        }
    }

    //Camera
    void camDirection()
    {
        camForward = m_cameraTransform.forward;
        camRight = m_cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    //Jump
    public void Jump()
    {
        m_rigidbody.AddForce(Vector3.up * f_jumpForce, ForceMode.Impulse);
        Vector3 velocity = m_rigidbody.velocity;
        velocity.y = 0;
        m_rigidbody.velocity = velocity;
    }

    public bool IsGrounded()
    {
      return Physics.CheckCapsule(m_playerCol.bounds.center, new Vector3(m_playerCol.bounds.center.x,
            m_playerCol.bounds.min.y, m_playerCol.bounds.center.z), m_playerCol.radius * 0.25f, m_groundLayer);
    }

    //HealItem
    public void HealItem()
    {
        heal = 10f;
        StartCoroutine(Healty());
    }
   
    //EnemyMele
    public void Enemymele()
    {
        damage = 15f;
        StartCoroutine(Golpe());
    }

    //Shadow Raycast
    void RaycastGround()
    {
        Ray ray = new Ray(m_transform.position, Vector3.down);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 10f, m_groundLayer))
        {
            m_shadowTransform.position = hit.point;
            m_shadowTransform.localRotation = Quaternion.FromToRotation(m_shadowTransform.up, hit.normal) * m_shadowTransform.localRotation;

        }
    }

    //GOOD MODE
    public void God()
    {
        m_playerspeed = 15f;
        
        gamemaster.bulletDamage = gamemaster.bulletGood;
        gamemaster.unlocked = true;

        gamemaster.swordDamage = gamemaster.swordDamageGood;


    }

    public void NoGod()
    {
        m_playerspeed = 5f;
        gamemaster.bulletDamage = gamemaster.bulletNoGood;
        gamemaster.swordDamage = gamemaster.swordDamageNoGood;

        
        if(gamemaster.value == 1)
        {
            gamemaster.unlocked = true;
        }
        gamemaster.unlocked = false;
    }

    //Dash
    public void CastDash()
    {
        if (staminabar.currentStamina >= 20)
        {
            StartCoroutine(Dash());
        }
    }
    //Run
    public void Run()
    {
        if (staminabar.currentStamina >= 5 && sprinting == true)
        {
            StartCoroutine(Sprint());
        }
    }

    //MeleAttack
    public void PlayerMeleAttack() 
    {
        if (f_cadenceTime > f_cadence)
        {
            f_cadenceTime = 0;
            StartCoroutine(MeleAttack()); 
        
        }    
        
    }

    //Corutina de golpe
    IEnumerator Golpe()
    {
        //Indico que recibo daño
        iamdead = true;
        
        //Indicamos al score que hemos perdido HP
        gamemaster.hp = gamemaster.hp - damage;
        healthbar.SendMessage("TakeDamage", damage);
        
        if(gamemaster.hp <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }

        //Player pushed
        m_rigidbody.AddForce(-transform.forward * 200f, ForceMode.Impulse);
        m_rigidbody.AddForce(transform.up * 5f, ForceMode.Impulse);

        yield return new WaitForSeconds(1.0f);
        iamdead = false;
    }
    //Corutina curacion
    IEnumerator Healty()
    {
        if (gamemaster.hp >= gamemaster.maxhp)
        {
            gamemaster.hp = gamemaster.maxhp;
        }
        else
        {
            gamemaster.hp = gamemaster.hp + heal;
        }
        healthbar.SendMessage("TakeLife", heal);
        yield return new WaitForSeconds(1.0f);
        //Sonido
        //Particulas
    }
    //Corutina Dash
    IEnumerator Dash()
    {
        m_rigidbody.AddForce(Camera.main.transform.forward * f_dashSpeed, ForceMode.VelocityChange);
        stamina.SendMessage("UseStamina", 20f);
        FindObjectOfType<AudioManager>().Play("Dash");
        yield return new WaitForSeconds(f_dashDuration);
        m_rigidbody.velocity = Vector3.zero;
    }
    //Corutina Sprint
    IEnumerator Sprint()
    {

        stamina.SendMessage("RunStamina", 5f);
        m_playerspeed = 10;
        yield return new WaitForSeconds(0);

       // m_rigidbody.velocity = Vector3.zero;
    }
    //MeleAttackCorutine
    IEnumerator MeleAttack()
    {
        transtion.SetBool("PlayMeleAttack", true);
        FindObjectOfType<AudioManager>().Play("Attack_1");
        yield return new WaitForSeconds(1.0f);
        transtion.SetBool("PlayMeleAttack", false);
    }
}
