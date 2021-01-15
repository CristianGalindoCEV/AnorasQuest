using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectAttack : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    private float f_timeCounter = 0;
    private float dietime = 7f;
    private Transform m_player;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        f_timeCounter += Time.deltaTime;
        

        if (speed != 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime); // Follow player
            transform.LookAt(m_player.transform);
        }
        else
        {
            Debug.Log("No speed");
        }
        //Destroy bullet
        if (f_timeCounter > dietime) { Destroy(gameObject); }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}

