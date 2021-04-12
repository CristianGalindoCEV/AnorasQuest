using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectAttack : MonoBehaviour
{
    private float speed = 15;
    private float f_timeCounter = 0;
    private float dietime = 5f;
    private Transform m_player;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        transform.LookAt(m_player.transform);
    }

    // Update is called once per frame
    void Update()
    {
        f_timeCounter += Time.deltaTime;

        if (speed != 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime); // Follow player
            
        }
        else
        {
            Debug.Log("No speed");
        }
        //Destroy bullet
        if (f_timeCounter > dietime) { Destroy(gameObject); }
    }
}

