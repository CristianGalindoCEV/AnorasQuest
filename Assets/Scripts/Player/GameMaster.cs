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

    void Start()
    {
        bulletDamage = bulletNoGood;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UnlockWeapon()
    {
        unlocked = true;
    }   

  

}
