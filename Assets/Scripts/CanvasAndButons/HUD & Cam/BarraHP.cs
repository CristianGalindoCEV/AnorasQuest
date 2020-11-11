using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraHP : MonoBehaviour
{
    public Image healt;
    public GameMaster gamemaster;
    // Start is called before the first frame update
    void Start()
    {
        //gamemaster.hp = gamemaster.maxhp;
    }

    void Update()
    {
        healt.fillAmount = gamemaster.hp / gamemaster.maxhp;
    }

    public void TakeDamage(float amount)
    {
        gamemaster.hp -= amount;
        healt.fillAmount = gamemaster.hp / gamemaster.maxhp;

    }
    public void TakeLife(float amount)
    {
        gamemaster.hp += amount;
        healt.fillAmount = gamemaster.hp / gamemaster.maxhp;

    }
}
