using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraSituate : MonoBehaviour
{
    public Transform loockAt;
    public Transform camTransform;
    public Transform playerTransform;
    public Transform mainCamera;

    Quaternion playerRotation;

    // Start is called before the first frame update
    void Start()
    {
        playerRotation = playerTransform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        //Situate AimCamera
        if (Input.GetButtonUp("Fire2"))
        {
            Vector3 rot = mainCamera.eulerAngles;
            rot.x = 0;
            rot.z = 0;

            playerRotation = Quaternion.Euler(rot);
        }
    }
}
