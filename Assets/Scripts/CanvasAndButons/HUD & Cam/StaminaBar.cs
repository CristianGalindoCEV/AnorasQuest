﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{

    public Slider staminaBar;
    public float maxStamina = 100;
    public float currentStamina;
    private Coroutine regen;

    private WaitForSeconds regenTick = new WaitForSeconds(0.1f);
    public static StaminaBar instance;


    // Start is called before the first frame update
    void Start()
    {
        currentStamina = maxStamina;
        staminaBar.maxValue = maxStamina;
        staminaBar.value = maxStamina;
    }

    public void UseStamina (int amount)
    {
        if (currentStamina - amount >= 0)
        {
            currentStamina -= amount;
            staminaBar.value = currentStamina;

            if (regen != null)
                StopCoroutine(regen);

          regen =  StartCoroutine(RegenStamina());
        }
        else
        {
            Debug.Log("No stamina");
        }

    }
    public void RunStamina( int amount)
    {
        if(currentStamina - amount >= 0)
        {
            currentStamina -= amount * Time.deltaTime;
            staminaBar.value = currentStamina;

            if (regen != null)
                StopCoroutine(regen);

            regen = StartCoroutine(RegenStamina());
        }
        else
        {
            Debug.Log("No stamina");
        }
        
    }


    private IEnumerator RegenStamina()
    {
        yield return new WaitForSeconds(1);

        while (currentStamina < maxStamina)
        {
            currentStamina += maxStamina / 100;
            staminaBar.value = currentStamina;
            yield return regenTick;
        }
        regen = null;
    }
}
