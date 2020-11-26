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

    //Disolve
    public Renderer m_renderer;
    private MaterialPropertyBlock m_materialProperty;
    private float m_disolve = -1f;

    void Start()
    {
        health = maxHealth;
        slider.value = CalculateHealth();
        healtbarUI.SetActive(false);
        //m_renderer = GetComponent<Renderer>();
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
            ActivateDisolve();
        }
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ActivateDisolve();
        }
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
        while (m_disolve < 1f)
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
