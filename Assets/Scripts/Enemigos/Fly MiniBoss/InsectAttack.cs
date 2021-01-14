using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectAttack : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private float f_timeCounter = 0;
    [SerializeField] private float dietime;
    public Transform player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        f_timeCounter += Time.deltaTime;

        if (speed != 0)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime); // Follow player
            transform.LookAt(player);
        }
        else
        {
            Debug.Log("No speed");
        }
        //Destroy bullet
        if (f_timeCounter > dietime) { Destroy(gameObject); }
    }
}

