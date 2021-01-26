using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsFinalBoss : MonoBehaviour
{
    private float f_timeCounter = 0;
    private Transform m_player;
    private Vector3 playerVector;


    //Easing
    Vector3 initValue;
    Vector3 finalValue;
    Vector3 currentValue;
    float maxTime = 4f;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        playerVector = m_player.position;
        
        initValue = transform.position;
        finalValue = playerVector;
        currentValue = initValue;
    }

    // Update is called once per frame
    void Update()
    {
        f_timeCounter += Time.deltaTime;

        currentValue.x = Easing.CubicEaseIn(f_timeCounter, initValue.x, finalValue.x - initValue.x, maxTime);
        currentValue.y = Easing.CubicEaseIn(f_timeCounter, initValue.y, finalValue.y - initValue.y, maxTime);
        currentValue.z = Easing.CubicEaseIn(f_timeCounter, initValue.z, finalValue.z - initValue.z, maxTime);
        transform.position = new Vector3(currentValue.x, currentValue.y, currentValue.z);

        if (f_timeCounter >= 5f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
        if (other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
}
