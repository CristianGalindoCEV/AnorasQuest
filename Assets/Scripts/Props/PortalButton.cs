using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalButton : MonoBehaviour
{
    public Transform wallPortal;
    private bool pulse = false;
    private Transform m_transform;

    float currentTime = 0;
    float initValue;
    float finalValue;
    float currentValue;
    float maxTime = 5f;
    private Vector3 upPortal;
    // Start is called before the first frame update
    void Start()
    {
        upPortal = wallPortal.transform.position;
        m_transform = transform;

        initValue = upPortal.y;
        finalValue = upPortal.y + 10;
        currentValue = initValue;
    }

    // Update is called once per frame
    void Update()
    {
        if(pulse == true)
        {
            currentTime += Time.deltaTime;

            if(currentTime <= maxTime)
            {
                currentValue = Easing.CubicEaseIn(currentTime, initValue, finalValue - initValue, maxTime);
            }
            wallPortal.position = new Vector3(wallPortal.position.x, currentValue, wallPortal.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && pulse == false)
        {
            transform.position = new Vector3(transform.position.x,transform.position.y - 1,transform.position.z);
            pulse = true;
        }
    }

}
