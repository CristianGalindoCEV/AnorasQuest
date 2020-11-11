using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : PhysicsCollision
{
   
    //Player
    private Rigidbody m_rigidbody;
    public float jumpForce = 5;
    private float m_horizontalMove;
    private float m_verticalMove;
    private Vector3 playerInput;
    private Transform m_transform;
    public float damage;
    public float heal;
    [SerializeField] private float m_playerspeed = 5;
    private Vector3 movePlayer;
    [SerializeField] private bool iamdead = false;

    //Camara
    [SerializeField] private Transform m_cameraTransform;
    [SerializeField]
    private Vector3 camForward;
    private Vector3 camRight;

    //Gravedad y salto
    [SerializeField] private float f_jumpTime = 0.5f;
    [SerializeField] private float m_gravityForce = 3f;
    [SerializeField] private float gravity = 70f;
    public float m_fallVelocity;
    [SerializeField] private float f_jumpForce = 20f;
    private float m_internGravity;

    /*
    private float f_jumpButtonPressTime;
    public bool jumpMinAirTime;
    public bool jumpMaxAirTime;
    private bool b_jumpButtonReleased;
    private bool b_jumping;
    private float f_jumpReleaseForce;
    private float f_jumpDefaultForce;
    */


    //Canvas
    public GameObject healthbar;
    public GameMaster gamemaster;

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

       // SetGravity();

        movePlayer.y = m_rigidbody.velocity.y;

        m_rigidbody.velocity = movePlayer;

       /* if(f_jumpButtonPressTime != 0 && Time.time - f_jumpButtonPressTime >= jumpMinAirTime &&
            Time.time -) f_jumpButtonPressTime <= jumpMaxAirTime && b_jumpButtonReleased || Time.time - f_jumpButtonPressTime && !m_checker.isGrounded)
        {
            b_jumpButtonReleased = false;
            JumpRelase();
        }*/

    }

    private void LateUpdate()
    {
        if (!isGrounded)
        {
            RaycastGround();

        }
        else
        {
            m_shadowGO.SetActive(false);
        }
    }

    //Camara
    void camDirection()
    {
        camForward = m_cameraTransform.forward;
        camRight = m_cameraTransform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    /*void SetGravity()
    {

        if (isGrounded)
        {
            m_fallVelocity = 0;

            m_internGravity = gravity * m_jumpTime;

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }

        if (Input.GetButton("Jump") && m_fallVelocity != 0)
        {
            ReleaseJump();
        }

        m_fallVelocity -= gravity * Time.deltaTime * m_gravityForce;
        movePlayer.y = m_fallVelocity;
    }*/

    //Salto
    public void ReleaseJump()
    {
        m_internGravity -= Time.deltaTime;
        m_fallVelocity += m_internGravity * Time.deltaTime;
    }
    public void Jump()
    {
        if (!isGrounded)
            return;

        m_rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    /*private void Jump (float force)
    {

        f_jumpForce = force;
        m_rigidbody.velocity = Vector3.zero;
        b_jumping = true;
    }

    public void JumpRelased()
    {
        if (b_jumpButtonReleased)
            return;
        Debug.Log("Released");
        Jump(-f_jumpReleaseForce);
    }

    public void JumpStart()
    {
        if (!m_checker.isGrounded)
        {
            return;
        }

        f_jumpButtonPressTime = Time.time;
        b_jumpButtonReleased = false;

        Jump(f_jumpDefaultForce);
    }*/
    
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



    //Corutina de golpe
    IEnumerator Golpe()
    {
        //Indico que estoy muerto
        iamdead = true;
        //Indicamos al score que hemos perdido HP
        gamemaster.hp = gamemaster.hp - damage;
        healthbar.SendMessage("TakeDamage", damage);
        //Player pushed
        m_rigidbody.AddForce(-transform.forward * 100f, ForceMode.Impulse);
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
}
