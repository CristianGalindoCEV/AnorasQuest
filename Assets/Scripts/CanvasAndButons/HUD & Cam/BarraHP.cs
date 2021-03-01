using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraHP : MonoBehaviour
{
    public Image healt;
    public PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        healt.fillAmount = playerStats.hp_stat / playerStats.maxhp_stat;
    }

    public void TakeDamage(float amount)
    {
        playerStats.hp_stat -= amount;
        healt.fillAmount = playerStats.hp_stat / playerStats.maxhp_stat;
    }
    public void TakeLife(float amount)
    {
        if (playerStats.hp_stat + amount >= playerStats.maxhp_stat)
        {
            playerStats.hp_stat = playerStats.maxhp_stat;
        }
        else
        {
            playerStats.hp_stat += amount;
        }
        healt.fillAmount = playerStats.hp_stat / playerStats.maxhp_stat;
    }
}
