using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    //Player
    public CharacterController player;
    public PlayerStats playerStats;
    public Animator animator;
    public Rigidbody playerBody;
    public GameObject J_Arm_R;
    private Vector3 m_playerInput;
    private Vector3 m_movePlayer;
    public Transform playerTransform;
    private float f_horizontalMove;
    private float f_verticalMove;

    [SerializeField]private float f_speed;

    public bool god = false;
    public Animator transtion;
    private float f_damage;
    private float f_hp;
    public bool aiming;
    
    //Camera
    public Camera mainCamera;
    public Camera aimCamera;
    private Vector3 camForward;
    private Vector3 camRight;

    //Gravedad y salto
    [SerializeField] private float f_gravity = 9.8f;
    private float f_fallVelocity;
    [SerializeField] private float f_jumpForce;
   
    //Canvas
    public GameObject healthbar;
    
    //Shadow
    [SerializeField] LayerMask m_groundLayer;

    // Start is called before the first frame update
    void Start()
    {
        aimCamera.enabled = false;
        transform.position = playerStats.playerPosition_stat;
    }

    void Update()
    {
        f_horizontalMove = Input.GetAxis("Horizontal");
        f_verticalMove = Input.GetAxis("Vertical");

        m_playerInput = new Vector3(f_horizontalMove, 0, f_verticalMove);
        m_playerInput = Vector3.ClampMagnitude(m_playerInput, 1);

        camDirection();
        m_movePlayer = m_playerInput.x * camRight + m_playerInput.z * camForward;
        m_movePlayer = m_movePlayer * f_speed;
        
        player.transform.LookAt(player.transform.position + m_movePlayer); // Player move with camera

        SetGravity();
        Jump();

        player.Move(m_movePlayer * Time.deltaTime);

        // GOOD MODE
        if (!god)
        {
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
                m_movePlayer.y += 20;

            if (Input.GetKey(KeyCode.N))
                m_movePlayer.y -= 20;

            while ( playerStats.hp_stat < playerStats.maxhp_stat)
            {
                playerStats.hp_stat++;
            }
        }
        //Animators
        animator.SetFloat("SpeedX", f_horizontalMove);
        animator.SetFloat("SpeedY", f_verticalMove);
        animator.SetBool("IsGrounded", player.isGrounded);
        animator.SetFloat("VelocityY", m_movePlayer.y);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet")
        {
            f_damage = 5f;
            StartCoroutine(Golpe());
        }
        if (other.tag == "Bicho")
        {
            f_damage = 1f;
            StartCoroutine(Golpe());
        }
        if (other.tag == "Hp")
        {
            f_hp = 25f;
            StartCoroutine(Heal());
        }
    }

    public void Jump()
    {
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("JumpStart");
            f_fallVelocity = f_jumpForce;
            m_movePlayer.y = f_fallVelocity;
        }
    }

    //Funcion de gravedad
    void SetGravity()
    {
        if (player.isGrounded)
        {
            f_fallVelocity = -f_gravity * Time.deltaTime;
            m_movePlayer.y = f_fallVelocity;
        }
        else
        {
            f_fallVelocity -= f_gravity * Time.deltaTime;
            m_movePlayer.y = f_fallVelocity;
        }
    }

    //Camera
    void camDirection()
    {
        if (aiming == false)
        {
            camForward = mainCamera.transform.forward;
            camRight = mainCamera.transform.right;

            camForward.y = 0;
            camRight.y = 0;

            camForward = camForward.normalized;
            camRight = camRight.normalized;
        }
        else if (aiming == true)
        {
            camForward = aimCamera.transform.forward;
            camRight = aimCamera.transform.right;

            camForward.y = 0;
            camRight.y = 0;

            camForward = camForward.normalized;
            camRight = camRight.normalized;
        }
    }
    
    //GOOD MODE
    public void God()
    {
        f_speed = 15f;
        playerStats.bulletDamage_stat = playerStats.bulletGood_stat;
    }
     
    public void NoGod()
    {
        f_speed = 8f;
        playerStats.bulletDamage_stat = playerStats.bulletNoGood_stat;
    }
       
    //Corutina de golpe
    IEnumerator Golpe()
    {
        //Indicamos al score que hemos perdido HP
        playerStats.hp_stat = playerStats.hp_stat - f_damage;
        healthbar.SendMessage("TakeDamage", f_damage);
        if(playerStats.hp_stat <= 0)
        {
            animator.SetBool("Death",true);
            player.enabled = false;
            yield return new WaitForSeconds(1.0f);
            SceneManager.LoadScene("GameOver");
        }//Die
        //Añadir Animacion Daño
        yield return new WaitForSeconds(1.0f);
    }
    
    //Heal
    public IEnumerator Heal()
    {
        playerStats.hp_stat = playerStats.hp_stat + f_hp;
        healthbar.SendMessage("TakeLife", f_hp);
        //Añadir Animacion Vida
        yield return new WaitForSeconds(1.0f);
    }
}
