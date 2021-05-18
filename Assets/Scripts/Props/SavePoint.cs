using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public PlayerStats playerStats;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerStats.savepoint_Position = other.transform.position;
        }
    }
}
