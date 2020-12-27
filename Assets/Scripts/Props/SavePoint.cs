using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePoint : MonoBehaviour
{
    public GameMaster gameMaster;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            gameMaster.playerPosition = other.transform.position;
            gameMaster.SavePoint();
        }
    }
}
