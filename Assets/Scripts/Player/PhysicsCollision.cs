using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsCollision : MonoBehaviour
{
    //fisicas del suelo

    [Header("Ground")]
    public bool isGrounded;
    public bool justGrounded;
    public bool wasGrounded;
    public LayerMask groundLayer;
    [Header("Wall")]
    public bool touchWall;
    public bool touchedWall;
    public bool justTouchWall;
    public LayerMask wallLayer;
    public bool isFacingRight = true;
    private Vector3 direction = Vector3.right; // esta variable definira la direccion donde miraran estos rayos
    public Transform graphicTransform;

    private Ray ray;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {

    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        CheckGround();
        CheckWall();
    }

    private void CheckGround()
    {
        wasGrounded = isGrounded; // si hemos cambiado de estar en suelo a no estarlo o si lo estamos en general
        isGrounded = false;

        Vector3 pos = transform.position;
        int sign = 1;

        for (int i = 0; i < 3; i++)
        {
            pos.x += i * 0.5f * sign;

            ray = new Ray(pos, Vector3.down);
            hit = new RaycastHit();

            isGrounded = Physics.Raycast(ray, out hit, 1.5f, groundLayer);

            if (isGrounded)
            {
                if (!wasGrounded)
                    justGrounded = true;

                return;
            }

        }
    }

    private void CheckWall() //
    {
        Vector3 position = transform.position;
        int sign = 1;

        touchedWall = touchWall;
        justTouchWall = false;

        for (int i = 0; i < 3; i++)
        {
            position.y += i * 0.5f * sign;
            ray = new Ray(position, direction);
            hit = new RaycastHit();

            touchWall = Physics.Raycast(ray, out hit, 0.5f, wallLayer);

            if (touchWall)
            {
                if (!touchedWall)
                    justTouchWall = true;

                break; // el break hace parar al for
            }
        }
    }

    private void OnDrawGizmos()
    {
        DrawGroundGizmos();
        DrawWallGizmos();
    }

    private void DrawGroundGizmos()
    {
        Gizmos.color = Color.magenta; // new Color32(254, 10 , 251, 255) -- esto otra forma de escribirlo, para para tener mas precision en el color

        Vector3 pos = transform.position;
        int sign = 1;

        for (int i = 0; i < 3; i++)
        {
            pos.x += 0.5f * i * sign;
            sign *= -1; // es para trandformar la poscion de los rayos por cada loop

            Gizmos.DrawRay(pos, Vector3.down); // es para que dibuje lo anterior
        }
    }

    private void DrawWallGizmos()
    {
        Gizmos.color = Color.green; // new Color32(254, 10 , 251, 255) -- esto otra forma de escribirlo, para para tener mas precision en el color

        Vector3 pos = transform.position;
        int sign = 1;

        for (int i = 0; i < 3; i++)
        {
            pos.y += 0.5f * i * sign;
            sign *= -1; // es para trandformar la poscion de los rayos por cada loop

            Gizmos.DrawRay(pos, direction * 0.5f); // es para que dibuje lo anterior
        }
    }
}
