using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MejoraItem : MonoBehaviour
{
    public bool IsDamage;
    public bool IsCadence;
    public bool IsTimeShot;

    private int i_attackPower = 80;
    private float f_cadence = 10;
    public PlayerStats playerStats;
    public Bauculo bauculo;
    public InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = FindObjectOfType<InputManager>();
        bauculo = FindObjectOfType<Bauculo>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && IsDamage == true)
        {
            playerStats.bulletDamage_stat = +i_attackPower;
        }
        
        if (other.tag == "Player" && IsCadence == true)
        {
            bauculo.Bulletspeed =+ f_cadence;
        }

        if (other.tag == "Player" && IsTimeShot == true)
        {
            playerStats.timeShot = 0.7f;
        }
    }
}
