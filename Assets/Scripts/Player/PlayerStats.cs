using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    //public bool unlocked_weapon;
    public float maxhp_stat = 100;
    public float hp_stat = 100;
   
    public int swordDamage_stat;
    public int swordDamageGood_stat = 100000;
    public int swordDamageNoGood_stat = 25;
   
    public int bulletDamage_stat;
    public int bulletGood_stat = 100000;
    public int bulletNoGood_stat = 15;

    public Vector3 playerPosition_stat;
    public bool revive = false;

    public static PlayerStats inst;
    
    
    // Start is called before the first frame update
    private void Awake()
    {
        if (PlayerStats.inst == null)
        {
            PlayerStats.inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
