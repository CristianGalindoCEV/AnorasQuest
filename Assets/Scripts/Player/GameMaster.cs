using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    private GameObject m_playerObject;
    public InputManager inputManager;

    public bool unlocked;
    public float maxhp;
    public float hp;
   
    public int swordDamage;
    public int swordDamageGood;
    public int swordDamageNoGood;
    
    public int bulletDamage;
    public int bulletGood;
    public int bulletNoGood;
    
    public PlayerStats playerStats;

    public Vector3 playerPosition;

    public int value = 0;

    //PlayerPref
    /*
    private string s_currentlife = "CurrentLife";
    private string s_maxLife = "MaxLife";
    private string s_unlock = "Unlock";
    */
    private void Awake()
    {
        playerStats = FindObjectOfType<PlayerStats>();
       
        maxhp = playerStats.maxhp_stat;
        hp = playerStats.hp_stat;
       
        bulletDamage = playerStats.bulletDamage_stat;
        bulletGood = playerStats.bulletGood_stat;
        bulletNoGood = playerStats.bulletNoGood_stat;
        
        swordDamage = playerStats.swordDamage_stat;
        swordDamageGood = playerStats.swordDamageGood_stat;
        swordDamageNoGood = playerStats.swordDamageNoGood_stat;

        m_playerObject = GameObject.FindWithTag("Player");

        if (playerStats.revive == true)
        {
            PlayerRevive();
            playerStats.revive = false;
        }

        //  LoadData();
    }
    void Start()
    {
        bulletDamage = bulletNoGood;
        swordDamage = swordDamageNoGood;

    }
    
    private void Update()
    {
        if (value == 1)
        {
            unlocked = true;
        }
    }
   
    public void SavePlayerStats()
    {
        playerStats.maxhp_stat = maxhp;
        playerStats.hp_stat = hp;
        
        playerStats.bulletDamage_stat = bulletDamage;
        playerStats.bulletGood_stat = bulletGood;
        playerStats.bulletNoGood_stat = bulletNoGood;

        playerStats.swordDamageNoGood_stat = swordDamageNoGood;
        playerStats.swordDamage_stat = swordDamage;
        playerStats.swordDamageGood_stat = swordDamageGood;
    }

    public void UnlockWeapon()
    {
        value = 1;
    }

    public void SavePoint()
    {
        playerStats.maxhp_stat = maxhp;
        playerStats.hp_stat = hp;

        playerStats.bulletDamage_stat = bulletDamage;
        playerStats.bulletGood_stat = bulletGood;
        playerStats.bulletNoGood_stat = bulletNoGood;

        playerStats.swordDamageNoGood_stat = swordDamageNoGood;
        playerStats.swordDamage_stat = swordDamage;
        playerStats.swordDamageGood_stat = swordDamageGood;

        playerStats.playerPosition_stat = playerPosition;
    }

    public void PlayerRevive()
    {
        hp = playerStats.hp_stat;
        playerPosition = playerStats.playerPosition_stat;
    }

    //SaveData
   /* private void SaveData()
    {
        PlayerPrefs.SetFloat(s_currentlife, hp);
        PlayerPrefs.SetFloat(s_maxLife, maxhp);
        PlayerPrefs.SetInt("Unlock", (unlocked ? 1 : 0));
    }
  
    //LoadData
    private void LoadData()
    {
        hp = PlayerPrefs.GetFloat(s_currentlife, hp);
        maxhp = PlayerPrefs.GetFloat(s_maxLife, maxhp);
        unlocked = (PlayerPrefs.GetInt("Unlock") != 0);
    }*/


}
