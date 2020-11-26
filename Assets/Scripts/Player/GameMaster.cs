using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public bool unlocked;
    public InputManager inputManager;
    public float maxhp = 100;
    public float hp = 100;
    public int bulletDamage;
    public int bulletGood = 100000;
    public int bulletNoGood = 15;

    public int value = 0;

    //PlayerPref
    private string s_currentlife = "CurrentLife";
    private string s_maxLife = "MaxLife";
    private string s_unlock = "Unlock";

    private void Awake()
    {
        LoadData();
    }
    void Start()
    {
        bulletDamage = bulletNoGood;
    }

    private void Update()
    {
        if (value == 1)
        {
            unlocked = true;
        }
    }
   
    //OnDestroy
    private void OnDestroy()
    {
        SaveData();
    }

    public void UnlockWeapon()
    {
        value = 1;
    }

    //SaveData
    private void SaveData()
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
    }


}
