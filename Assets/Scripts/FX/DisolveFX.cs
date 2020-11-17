using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisolveFX : MonoBehaviour
{
    private Renderer m_renderer;
    private MaterialPropertyBlock m_materialProperty;
    private float m_disolve = -1f;

    // Start is called before the first frame update
    void Start()
    {
        m_renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            ActivateDisolve();
        }
    }

    public void ActivateDisolve()
    {
        m_materialProperty = new MaterialPropertyBlock();
        m_disolve = -1;

        StartCoroutine(UpdateDisolve());
    }

    private IEnumerator UpdateDisolve()
    {


        while(m_disolve < 1f)
        {
            m_disolve += Time.deltaTime;

            m_materialProperty.SetFloat("_Disolve", m_disolve);
            m_renderer.SetPropertyBlock(m_materialProperty);

            yield return null;
        }
    }
}
