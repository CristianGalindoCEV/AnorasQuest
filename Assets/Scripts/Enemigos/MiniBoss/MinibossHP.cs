using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinibossHP : MonoBehaviour
{
    public Slider bossBar;
    public float maxHp = 3000;
    public float hp;


    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        bossBar.value = CalculateHealth();
    }

    // Update is called once per frame
    void Update()
    {
        bossBar.value = CalculateHealth();

        if (hp <= 0)
        {
            Debug.Log("SeMuere");
        }
      
        if (hp > maxHp)
        {
            hp = maxHp;
        }
    }
 
    float CalculateHealth()
    {
        return hp / maxHp;
    }
  
    public void TakeDamage(int amount)
    {
        hp -= amount;

    }
}
