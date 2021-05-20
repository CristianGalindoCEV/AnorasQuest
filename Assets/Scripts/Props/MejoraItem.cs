using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MejoraItem : MonoBehaviour
{
    public bool IsDamage;
    public bool IsCadence;
    public bool IsTimeShot;

    //Image Bufs Interface
    public PlayerStats playerStats;
    private Bauculo bauculo;
    private MenuManager menuManager;

    private int i_attackPower = 80;
    private float f_cadence = 10;

    // Start is called before the first frame update
    void Start()
    {
        bauculo = FindObjectOfType<Bauculo>();
        menuManager = FindObjectOfType<MenuManager>();

        //PlayerStats Instance
        if (IsCadence == true && playerStats.CadenceBuf == true)
        {
            Destroy(gameObject);
        }
        if (IsDamage == true && playerStats.DamageBuf == true)
        {
            Destroy(gameObject);
        }
        if (IsTimeShot == true && playerStats.SpeedBulletBuf == true)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && IsDamage == true)
        {
            playerStats.bulletDamage_stat = +i_attackPower;
            playerStats.bulletNoGood_stat = +i_attackPower;
            playerStats.DamageBuf = true;
            menuManager.UnlockedDamage(); //Animation
        }
        
        if (other.tag == "Player" && IsCadence == true)
        {
            bauculo.Bulletspeed =+ f_cadence;
            playerStats.CadenceBuf = true;
            menuManager.UnlockedCadence(); // Animation
        }

        if (other.tag == "Player" && IsTimeShot == true)
        {
            playerStats.timeShot = 0.7f;
            playerStats.SpeedBulletBuf = true;
            menuManager.UnlockedSpeed(); // Animation
        }
    }
}
