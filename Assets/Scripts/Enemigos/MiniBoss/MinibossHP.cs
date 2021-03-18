using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinibossHP : MonoBehaviour
{
    public Slider bossBar;
    public float maxHp = 2000;
    public float hp;
    private GameObject portal;

    //Disolve
    public Renderer m_renderer;
    private MaterialPropertyBlock m_materialProperty;
    private float m_disolve = -1f;
    private bool b_activateDisolve = false;

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHp;
        bossBar.value = CalculateHealth();
        portal = GameObject.Find("Portal_Prop");
        portal.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        bossBar.value = CalculateHealth();

        if (hp <= 0 && b_activateDisolve == false)
        {
            portal.SetActive(true);
            ActivateDisolve();
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
    public void ActivateDisolve()
    {
        m_materialProperty = new MaterialPropertyBlock();
        m_disolve = -1;
        b_activateDisolve = true;
        StartCoroutine(UpdateDisolve());
    }

    //Disolve
    private IEnumerator UpdateDisolve()
    {
        while (m_disolve <= 1f)
        {
            m_disolve += Time.deltaTime/2;
            m_materialProperty.SetFloat("_Disolve", m_disolve);
            m_renderer.SetPropertyBlock(m_materialProperty);
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
