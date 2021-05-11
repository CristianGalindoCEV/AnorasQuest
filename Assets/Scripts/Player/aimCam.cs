using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimCam: MonoBehaviour
{
    private const float Y_ANGLE_MIN = -60f;
    private const float Y_ANGLE_MAX = 60f;

    public Transform loockAt;
    public Transform camTransform;
    public Transform playerTransform;
    public Transform mainCamera;

    Quaternion playerRotation;
    Quaternion cameraRotation;

    public float distance;
    private float currentX = 0f;
    private float currentY = 0f;
    [SerializeField] private float f_mouseSensivility = 2f;
    [SerializeField] private Transform normalCameraTransform;

    public bool justAiming;
    public bool wasAiming;

    public PlayerController playerController;

    // Use this for initialization
    void Start()
    {
        playerRotation = playerTransform.localRotation;
        cameraRotation = camTransform.localRotation;
    }
    private void Update()
    {
        wasAiming = playerController.aiming;
        justAiming = false;

        if (playerController.aiming == true)
        {
            currentX = Input.GetAxis("Mouse X") * f_mouseSensivility;
            currentY = Input.GetAxis("Mouse Y") * f_mouseSensivility;

            currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

            if (!wasAiming)
                justAiming = true;
        }
        else if (playerController.aiming == false)
        {
            //Añadir funcion para tener la misma posicion que la camara de apuntado
        }

        if (justAiming)
        {
            Debug.Log("loooooooo");
            playerRotation = playerTransform.localRotation;
        }
    }

    private void LateUpdate()
    {
        if (playerController.aiming == true)
        {
            /*
            Vector3 dir = new Vector3(0, 0, -distance);
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            camTransfom.position = loockAt.position + rotation * dir;
            camTransfom.LookAt(loockAt.position);
            */

            cameraRotation *= Quaternion.Euler(-currentY, 0, 0);
            playerRotation *= Quaternion.Euler(0, currentX, 0);

            camTransform.localRotation = cameraRotation;
            playerTransform.localRotation = playerRotation;
        }
    }
}
