using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aimCam: MonoBehaviour
{
    private const float Y_ANGLE_MIN = -60f;
    private const float Y_ANGLE_MAX = 60f;

    private const float X_ANGLE_MIN = -360f;
    private const float X_ANGLE_MAX = 360f;

    public Transform loockAt;
    public Transform camTransfom;

    public Camera cam;

    public float distance;
    private float currentX = 0f;
    private float currentY = 0f;

    // Use this for initialization
    void Start()
    {
        camTransfom = transform;
    }
    private void Update()
    {
        currentX += Input.GetAxis("Mouse X");
        currentY += Input.GetAxis("Mouse Y");

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        currentX = Mathf.Clamp(currentX, X_ANGLE_MIN, X_ANGLE_MAX);
    }
    private void LateUpdate()
    {
        Vector3 dir = new Vector3( 0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransfom.position = loockAt.position + rotation * dir;
        camTransfom.LookAt(loockAt.position);
        //camTransfom.rotation =  Quaternion.Euler(loockAt.rotation.x, camTransfom.rotation.y, camTransfom.rotation.z);
    }
}
