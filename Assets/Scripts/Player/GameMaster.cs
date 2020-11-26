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

    //PlayerPref
    private string s_currentlife = "CurrentLife";
    private string s_maxLife = "MaxLife";

    private void Awake()
    {
        LoadData();
    }
    void Start()
    {
        bulletDamage = bulletNoGood;
    }
    
    //OnDestroy
    private void OnDestroy()
    {
        SaveData();
    }

    public void UnlockWeapon()
    {
        unlocked = true;
    }

    //SaveData
    private void SaveData()
    {
        PlayerPrefs.SetFloat(s_currentlife, hp);
        PlayerPrefs.SetFloat(s_maxLife, maxhp);
    }
    //LoadData
    private void LoadData()
    {
        hp = PlayerPrefs.GetFloat(s_currentlife, hp);
        maxhp = PlayerPrefs.GetFloat(s_maxLife, maxhp);
    }


}
