using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Player
    public CharacterController player;
    public Transform playerTransform;

    private float m_horizontalMove;
    private float m_verticalMove;
    [SerializeField] private float f_speed;
    
    private Vector3 m_playerInput;
    private Vector3 m_movePlayer;
    
    private bool iamdead = false;
    public bool god = false;
    public Animator transtion;
    public float damage;
    //Dash
    [SerializeField] private float f_dashSpeed;
    [SerializeField] private float f_dashDuration;

    //Camera
    //[SerializeField] private Transform m_cameraTransform;
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    //Gravedad y salto
    [SerializeField] private float m_jumpTime = 0.5f;
    [SerializeField] private float m_gravityForce = 3f;
    [SerializeField] private float gravity = 70f;
    public float m_fallVelocity;
    [SerializeField] private float m_jumpForce = 20f;
    private float m_internGravity;

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

    }

    private void Update()
    {
        m_horizontalMove = Input.GetAxis("Horizontal");
        m_verticalMove = Input.GetAxis("Vertical");

        m_playerInput = new Vector3(m_horizontalMove, 0, m_verticalMove);
        m_playerInput = Vector3.ClampMagnitude(m_playerInput, 1);
        
        camDirection();

        m_movePlayer = m_playerInput.x * camRight + m_playerInput.z * camForward;
        m_movePlayer = m_movePlayer * f_speed;

        player.transform.LookAt(player.transform.position + m_movePlayer);
        SetGravity();
        
        player.Move(m_movePlayer * Time.deltaTime);
        
        // GOOD MODE
        if (!god)
        {
            //m_movePlayer.y = m_rigidbody.velocity.y;
            //f_cadenceTime += Time.deltaTime;
        }
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
                //m_movePlayer.y += 20;

            if (Input.GetKey(KeyCode.N))
                //m_movePlayer.y -= 20;

            while ( gamemaster.hp < gamemaster.maxhp)
            {
                gamemaster.hp++;
            }

        }
    }
    private void FixedUpdate ()
    {
        player.Move(new Vector3(m_horizontalMove, 0, m_verticalMove) * f_speed * Time.deltaTime);
    }
    private void LateUpdate()
    {
        if (!player.isGrounded)
        {
            m_shadowGO.SetActive(true);
            RaycastGround();
        }
        else
        {
            m_shadowGO.SetActive(false);
        }
    }

    //Funcion de gravedad
    void SetGravity()
    {
        if (player.isGrounded)
        {
            m_fallVelocity = 0;
            m_internGravity = gravity * m_jumpTime;
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
        else
        {
            m_fallVelocity -= gravity * Time.deltaTime  * m_gravityForce ;
            m_movePlayer.y = m_fallVelocity;
        }
        if (Input.GetButton("Jump") && m_fallVelocity != 0)
        {
            ReleaseJump();
        }
    }

    //Camera
    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    public void Jump()
    {
        m_fallVelocity = m_jumpForce;
        m_shadowGO.SetActive(true);
    }
    public void ReleaseJump()
    {
        m_internGravity -= Time.deltaTime;
        m_fallVelocity += m_internGravity * Time.deltaTime;
    }

    //Shadow Raycast
    void RaycastGround()
    {
        Ray ray = new Ray(playerTransform.position, Vector3.down);
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
        f_speed = 15f;
        gamemaster.bulletDamage = gamemaster.bulletGood;
        gamemaster.unlocked = true;
        gamemaster.swordDamage = gamemaster.swordDamageGood;
    }

    public void NoGod()
    {
        f_speed = 5f;
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

    //Corutina de golpe
    IEnumerator Golpe()
    {
        iamdead = true;
        //Indicamos al score que hemos perdido HP
        gamemaster.hp = gamemaster.hp - damage;
        healthbar.SendMessage("TakeDamage", damage);
        if(gamemaster.hp <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
        
        //Añadir Animacion Daño
        yield return new WaitForSeconds(1.0f);
        iamdead = false;
    }

    //Corutina Dash
    IEnumerator Dash()
    {
        stamina.SendMessage("UseStamina", 20f);
        FindObjectOfType<AudioManager>().Play("Dash");
        yield return new WaitForSeconds(f_dashDuration);
    }
}
