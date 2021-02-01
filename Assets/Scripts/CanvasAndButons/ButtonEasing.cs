using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEasing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float m_initScale = 1;
    private float m_finalScale = 2;
    private float m_currentScale;

    private float m_currentTime;
    private float m_maxTime = 1;

    [SerializeField]private bool myanimation = false;
    // Start is called before the first frame update
    void Start()
    {
        m_currentScale = m_initScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(myanimation == true)
        {
            m_currentTime += Time.deltaTime;
            
            m_currentScale = Easing.CircEaseInOut(m_currentTime, m_initScale, m_finalScale - m_initScale, m_maxTime);
            transform.localScale = new Vector3(m_currentScale, m_currentScale, m_currentScale);
            if (m_currentTime >= m_maxTime)
            {
                m_finalScale = m_initScale;
                m_initScale = m_currentScale;
                m_currentTime = 0;
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        myanimation = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        myanimation = false;
        m_currentScale = 0;
        m_currentTime = 0;
        m_initScale = 1;
        m_finalScale = 2;
        transform.localScale = new Vector3(1,1,1);
}
}
