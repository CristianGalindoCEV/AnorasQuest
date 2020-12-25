using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBoss : MonoBehaviour
{
    private float hp = 200;
    public GameMaster gameMaster;
    public bool destroyWall = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hp <= 0)
        {
            destroyWall = true;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            hp = hp - gameMaster.bulletDamage;
        }
        if (other.tag == "Sword")
        {
            hp = hp - gameMaster.swordDamage;
        }
    }
}
