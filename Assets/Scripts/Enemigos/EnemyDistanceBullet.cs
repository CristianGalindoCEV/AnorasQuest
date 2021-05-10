using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDistanceBullet : MonoBehaviour
{
    [SerializeField] private float speed = 10;
    private float f_timeCounter = 0;
    [SerializeField] private float f_dietime = 4.5f;
    private Transform m_player;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("COG").transform;
        transform.LookAt(m_player);
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
        if (f_timeCounter > f_dietime) { Destroy(gameObject); }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
