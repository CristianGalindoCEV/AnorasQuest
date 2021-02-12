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

    float button_initValue;
    float button_finalValue;
    float button_currentValue;
    float button_maxTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        upPortal = wallPortal.transform.position;
        m_transform = transform;

        initValue = upPortal.y;
        finalValue = upPortal.y + 10;
        currentValue = initValue;

        button_currentValue = button_initValue;
        button_finalValue = transform.position.y - 1;
        button_initValue = transform.position.y;
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
            if (currentTime <= button_maxTime)
            {
                button_currentValue = Easing.CubicEaseIn(currentTime, button_initValue, button_finalValue - button_initValue, button_maxTime);
            }
            wallPortal.position = new Vector3(wallPortal.position.x, currentValue, wallPortal.position.z);
            transform.position = new Vector3(transform.position.x, button_currentValue, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && pulse == false)
        {
            pulse = true;
            FindObjectOfType<AudioManager>().Play("UnlockPortal");
        }
    }

}
