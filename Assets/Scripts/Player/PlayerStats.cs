using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Anora", menuName = "Player")]
public class PlayerStats : ScriptableObject
{
    //HP
    public float maxhp_stat = 100;
    public float hp_stat = 100;
   
    //Bullets
    public int bulletDamage_stat;
    public int bulletGood_stat = 100000;
    public int bulletNoGood_stat = 15;
    public float timeShot = 1f;

    //Respawn
    public Vector3 playerPosition_stat;
    public bool revive = false;

    //Tutorial
    public bool tutorial = false;

    public static PlayerStats inst;
}
