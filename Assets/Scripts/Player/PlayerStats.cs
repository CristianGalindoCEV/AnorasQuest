using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Anora", menuName = "Player")]
public class PlayerStats : ScriptableObject
{
    //public bool unlocked_weapon;
    public float maxhp_stat = 100;
    public float hp_stat = 100;
   
    public int bulletDamage_stat;
    public int bulletGood_stat = 100000;
    public int bulletNoGood_stat = 15;

    public Vector3 playerPosition_stat;
    public int revive = 0;

    public static PlayerStats inst;
}
