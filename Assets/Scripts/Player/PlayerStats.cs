using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Anora", menuName = "Player")]
public class PlayerStats : ScriptableObject
{
    //Game Audio Settings
    public bool gameStart = false; //Control audio settings if dont start game (Don't touch) important build game this setting false

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
    public Vector3 savepoint_Position;
    public bool revive = false;

    //Bufs UI Image Bufs
    public bool DamageBuf = false;
    public bool CadenceBuf = false;
    public bool SpeedBulletBuf = false;

    //Tutorial
    public bool tutorial = false;

    //Boss Completed = Desapear portal main map
    public bool FlyBoss;
    public bool StaticBoss;

    public static PlayerStats inst;
}
