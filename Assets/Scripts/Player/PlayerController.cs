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
    private Vector3 playerInput;
    private Transform m_transform;
    private float damage;
    private float heal;
    [SerializeField] private float m_playerspeed = 5;
    private Vector3 movePlayer;
    [SerializeField] private bool iamdead = false;
    public PhysicsCollision pysicsCollision;
    public bool god = false;
    public Animator transtion;
    public float cadencia;
    private float m_tiempoCadencia = 0;

    //Dash y sprint
    [SerializeField] private float f_dashSpeed;
    [SerializeField] private float f_dashDuration;
    [SerializeField] private float f_sprint;
    public bool sprinting = false;

    //Camara
    [SerializeField] private Transform m_cameraTransform;
    [SerializeField]
    private Vector3 camForward;
    private Vector3 camRight;

    //Gravedad y salto
    [SerializeField] private float f_jumpForce = 20f;
    private float f_jumpButtonPressTime;
    public float jumpMinAirTime;
    public float jumpMaxAirTime;
    private bool b_jumpButtonReleased;
    [SerializeField]
    private float f_jumpReleaseForce;
    [SerializeField]
    private float f_jumpDefaultForce;
    private bool b_jumping;

    //Canvas
    public GameObject healthbar;
    public GameMaster gamemaster;
    public GameObject stamina;
    public StaminaBar staminabar;

    //Sombra
    [SerializeField] GameObject m_shadowGO;
    [SerializeField] Transform m_shadowTransform;
    [SerializeField] LayerMask m_groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_transform = transform;

    }

    private void Update()
    {
        m_horizontalMove = Input.GetAxis("Horizontal");
        m_verticalMove = Input.GetAxis("Vertical");

        playerInput = new Vector3(m_horizontalMove, 0, m_verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * m_playerspeed;

        m_transform.LookAt(m_transform.position + movePlayer);
        
        if (!god)
            movePlayer.y = m_rigidbody.velocity.y;

        m_tiempoCadencia += Time.deltaTime;

       /* //Jump
        if (f_jumpButtonPressTime != 0 && Time.time - f_jumpButtonPressTime >= jumpMinAirTime &&
            Time.time - f_jumpButtonPressTime <= jumpMaxAirTime && b_jumpButtonReleased || Time.time - f_jumpButtonPressTime >= jumpMaxAirTime && !isGrounded)
        {
            b_jumpButtonReleased = false;
            JumpRelased();
        }
       */
       
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
                movePlayer.y += 20;

            if (Input.GetKey(KeyCode.N))
                movePlayer.y -= 20;

            while ( gamemaster.hp < gamemaster.maxhp)
            {
                gamemaster.hp++;
            }

        }

        m_rigidbody.velocity = movePlayer;

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
    /*
    private void Jump (float force)
    {

        f_jumpForce = force;
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.AddForce(f_jumpForce * Vector3.up);
        b_jumping = true;
    }

    public void JumpRelased()
    {
        if (b_jumpButtonReleased || god)
            return;
        Jump(-f_jumpReleaseForce);
    }

    public void JumpStart()
    {
        if (!isGrounded || god)
        {
            return;
        }

        f_jumpButtonPressTime = Time.time;
        b_jumpButtonReleased = false;

        Jump(f_jumpDefaultForce);
    }
    */
   
    public void Jump()
    {
        m_rigidbody.AddForce(Vector3.up * f_jumpForce, ForceMode.Impulse);
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

        if (Physics.Raycast(ray, out hit, 100f, m_groundLayer))
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
        if (m_tiempoCadencia > cadencia)
        {
            m_tiempoCadencia = 0;
            StartCoroutine(MeleAttack()); 
        
        }    
        
    }

    //Corutina de golpe
    IEnumerator Golpe()
    {
        //Indico que estoy muerto
        iamdead = true;
        //Indicamos al score que hemos perdido HP
        gamemaster.hp = gamemaster.hp - damage;
        healthbar.SendMessage("TakeDamage", damage);
        //Player pushed
        m_rigidbody.AddForce(-transform.forward * 200f, ForceMode.Impulse);
        m_rigidbody.AddForce(transform.up * 5f, ForceMode.Impulse);

        if (gamemaster.hp <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
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
