﻿using System.Collections;
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

    //Dash

    [SerializeField] private float f_dashSpeed;
    [SerializeField] private float f_dashDuration;

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

        movePlayer.y = m_rigidbody.velocity.y;

        m_rigidbody.velocity = movePlayer;

       if(f_jumpButtonPressTime != 0 && Time.time - f_jumpButtonPressTime >= jumpMinAirTime &&
            Time.time - f_jumpButtonPressTime <= jumpMaxAirTime && b_jumpButtonReleased || Time.time - f_jumpButtonPressTime >= jumpMaxAirTime && !isGrounded)
        {
            b_jumpButtonReleased = false;
            JumpRelased();
        }

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
    private void Jump (float force)
    {

        f_jumpForce = force;
        m_rigidbody.velocity = Vector3.zero;
        m_rigidbody.AddForce(f_jumpForce * Vector3.up);
        b_jumping = true;
    }

    public void JumpRelased()
    {
        if (b_jumpButtonReleased)
            return;
        Jump(-f_jumpReleaseForce);
    }

    public void JumpStart()
    {
        if (!isGrounded)
        {
            return;
        }

        f_jumpButtonPressTime = Time.time;
        b_jumpButtonReleased = false;

        Jump(f_jumpDefaultForce);
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
   
    public void CastDash()
    {
        if (staminabar.currentStamina >= 10)
        {
            StartCoroutine(Dash());
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

    IEnumerator Dash()
    {
        m_rigidbody.AddForce(Camera.main.transform.forward * f_dashSpeed, ForceMode.VelocityChange);
        stamina.SendMessage("UseStamina", 20f);
        yield return new WaitForSeconds(f_dashDuration);

        m_rigidbody.velocity = Vector3.zero;
    }
}
