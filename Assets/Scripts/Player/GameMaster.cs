using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    private GameObject m_playerObject;
    public InputManager inputManager;

    public float maxhp;
    public float hp;
    
    public int bulletDamage;
    public int bulletGood;
    public int bulletNoGood;
    
    public PlayerStats playerStats;

    public Vector3 playerPosition;

    public int value = 0;

    private void Awake()
    {  
        maxhp = playerStats.maxhp_stat;
        hp = playerStats.hp_stat;
       
        bulletDamage = playerStats.bulletDamage_stat;
        bulletGood = playerStats.bulletGood_stat;
        bulletNoGood = playerStats.bulletNoGood_stat;

        m_playerObject = GameObject.FindWithTag("Player");
/*
        if (playerStats.revive == true)
        {
            PlayerRevive();
        }
*/
    }
    void Start()
    {
        bulletDamage = bulletNoGood;
    }
    private void OnEnable()
    {
        /*
        if (playerStats.revive == true)
        {
            m_playerObject.transform.position = playerPosition;
            Debug.Log("Hola");
        }
        */
    }
    private void Update()
    {

    }
    public void SavePlayerStats()
    {
        playerStats.maxhp_stat = maxhp;
        playerStats.hp_stat = hp;
        
        playerStats.bulletDamage_stat = bulletDamage;
        playerStats.bulletGood_stat = bulletGood;
        playerStats.bulletNoGood_stat = bulletNoGood;
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

        playerStats.playerPosition_stat = playerPosition;
    }
    public void PlayerRevive()
    {
        playerPosition = playerStats.playerPosition_stat;
        hp = playerStats.hp_stat;
    }
}
