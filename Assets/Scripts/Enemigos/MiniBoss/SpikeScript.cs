using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    private float TimeCounter = 0;
    private GameObject m_player;

    private void Start()
    {
        //m_player = GameObject.Find("Player");
    }
    private void OnEnable()
    {
        //transform.position = new Vector3 (m_player.transform.position.x, m_player.transform.position.y - 2, m_player.transform.position.z);
    }
    // Update is called once per frame
    void Update()
    {
        TimeCounter += Time.deltaTime;
        if (TimeCounter < 2){
            transform.Translate(Vector3.up * Time.deltaTime);
        }
        if (TimeCounter > 3){Destroy(gameObject);}     
    }
}
