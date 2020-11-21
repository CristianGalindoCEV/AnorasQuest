using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public bool unlocked;
    public InputManager inputManager;
    public float maxhp = 100;
    public float hp = 100;
    public int bulletDamage = 15;
    // Start is called before the first frame update
    void Start()
    {
        
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
