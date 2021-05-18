using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointBigLevel : MonoBehaviour
{
    //Position
    public PlayerStats playerStats;
    public Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        if (playerStats.FlyBoss == false && playerStats.StaticBoss == false) // If u don't kill any boss
        {
            playerStats.playerPosition_stat = position;
        }
        else
        {
            position = playerStats.savepoint_Position;
        }
    }
}
