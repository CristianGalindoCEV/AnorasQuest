using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimCam: MonoBehaviour
{
    private const float Y_ANGLE_MIN = -90f;
    private const float Y_ANGLE_MAX = 90;

    public Transform loockAt;
    public Transform camTransform;
    public Transform playerTransform;
    public Transform mainCamera;

    Quaternion playerRotation;
    Quaternion cameraRotation;

    public float distance;
    [SerializeField] private float currentX = 0f;
    [SerializeField] private float currentY = 0f;
    [SerializeField] private float f_mouseSensivility = 2f;

    public PlayerController playerController;

    // Use this for initialization
    void Start()
    {
        playerRotation = playerTransform.localRotation;
        cameraRotation = camTransform.localRotation;
    }
    private void Update()
    {
        if (playerController.aiming == true)
        {
            currentX = Input.GetAxis("Mouse X") * f_mouseSensivility;
            currentY = Input.GetAxis("Mouse Y") * f_mouseSensivility;

            //currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

            cameraRotation *= Quaternion.Euler(-currentY, 0, 0);
            playerRotation *= Quaternion.Euler(0, currentX, 0);

            cameraRotation = ClampRotationArroundXAxis(cameraRotation);

            camTransform.localRotation = cameraRotation;
            playerTransform.localRotation = playerRotation;
            
            
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Vector3 rot = mainCamera.eulerAngles;
            rot.x = 0;
            rot.z = 0;

            playerRotation = Quaternion.Euler(rot);
        }
    }

    Quaternion ClampRotationArroundXAxis(Quaternion q)
    {
        q.x /= q.w;
        q.y /= q.w;
        q.z /= q.w;
        q.w = 1.0f;

        float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

        angleX = Mathf.Clamp(angleX, Y_ANGLE_MIN, Y_ANGLE_MAX);

        q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

        q.y = 0;
        q.z = 0;

        return q;
    }
}
