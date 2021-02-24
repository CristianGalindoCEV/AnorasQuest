using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    [SerializeField] private GameObject m_playerObject;
    public InputManager inputManager;

    public bool unlocked;
    public float maxhp;
    public float hp;
    
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

        m_playerObject = GameObject.FindWithTag("Player");

        if (playerStats.revive == true)
        {
            PlayerRevive();
            //playerStats.revive = false;
        }
        //  LoadData();
    }
    void Start()
    {
        bulletDamage = bulletNoGood;
    }
    private void OnEnable()
    {
        if (playerStats.revive == true)
        {
            m_playerObject.transform.position = playerPosition;
            Debug.Log("Hola");
        }
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
