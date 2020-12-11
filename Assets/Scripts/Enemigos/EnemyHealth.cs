﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.UI;

public class EnemyHealth: MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject healtbarUI;
    public Slider slider;

    //Disolve
    public Renderer m_renderer;
    private MaterialPropertyBlock m_materialProperty;
    private float m_disolve = -1f;
    [SerializeField]private GameObject m_burn;

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
            m_burn.SetActive(true);
            ActivateDisolve();
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
      /* if (Input.GetKey(KeyCode.M))
        {
            ActivateDisolve();
        }
      */
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }

    //ActivateDisolve
    public void ActivateDisolve()
    {
        m_materialProperty = new MaterialPropertyBlock();
        m_disolve = -1;

        StartCoroutine(UpdateDisolve());
    }

    //Disolve
    private IEnumerator UpdateDisolve()
    {
        while (m_disolve < 1.5f)
        {
            m_disolve += Time.deltaTime;

            m_materialProperty.SetFloat("_Disolve", m_disolve);
            m_renderer.SetPropertyBlock(m_materialProperty);

            yield return null;
        }

        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);

    }

}
