using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    private float TimeCounter = 0;
    private PlayerController m_playerController;
    // Update is called once per frame
    void Start()
    {
     m_playerController = FindObjectOfType<PlayerController>();   
    }

    void Update()
    {
        TimeCounter += Time.deltaTime;
        if (TimeCounter < 2){
            transform.Translate(Vector3.up * Time.deltaTime);
        }
        if (TimeCounter > 3){Destroy(gameObject);}     
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            m_playerController.Spike();
        }
    }

}
