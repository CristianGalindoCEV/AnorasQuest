using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth: MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject healtbarUI;
    public Slider slider;



    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
        healtbarUI.SetActive(false);
    }


    void Update()
    {

        slider.value = CalculateHealth();

        if (health < maxHealth)
        {
            healtbarUI.SetActive(true);
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            //Montar coorutina para sonidos y particulas
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }
}
