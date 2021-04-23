using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBoses : MonoBehaviour
{
    public PlayerStats playerStats;
    public Vector3 position;
    // Start is called before the first frame update
    void Start()
    {
        playerStats.playerPosition_stat = position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
