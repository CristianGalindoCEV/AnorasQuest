using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WallBoss : MonoBehaviour
{
    [SerializeField] private float hp;
    [SerializeField] private float maxHP = 300;
    public PlayerStats playerStats;
    public bool destroyWall = false;
    public bool startWall = false;
    public Slider bossBar;
    public GameObject WallSlider;

    // Start is called before the first frame update
    void Start()
    {
        WallSlider.SetActive(false);
        hp = maxHP;
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bossBar.value = CalculateHealth();

        if (hp <= 0 )
        {
            WallSlider.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }

    float CalculateHealth()
    {
        return hp / maxHP;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            hp = hp - playerStats.bulletDamage_stat;
        }
    }
}
