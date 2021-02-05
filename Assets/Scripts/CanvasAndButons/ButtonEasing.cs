using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonEasing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private float m_initScale = 1;
    private float m_finalScale = 2;
    private float m_currentScale;

    public Image selector;
    private float m_initMove;
    private float m_finalMove;
    private float m_currentMove;

    private float m_currentTime;
    private float m_maxTime = 1;
    [SerializeField]private bool myanimation = false;
    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        m_currentScale = m_initScale;
        m_initMove = selector.transform.position.x - 10;
        m_finalMove = selector.transform.position.x + 3;
        m_currentMove = m_initMove;
        selector.enabled = false;
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(myanimation == true)
        {
            m_currentTime += Time.deltaTime;

            /*
            m_currentScale = Easing.CircEaseInOut(m_currentTime, m_initScale, m_finalScale - m_initScale, m_maxTime);
            rect.localScale = new Vector3(m_currentScale, m_currentScale, m_currentScale);
            */
            m_currentMove = Easing.CubicEaseInOut(m_currentTime, m_initMove, m_finalMove - m_initMove, m_maxTime);
            selector.transform.position = new Vector3(m_currentMove, selector.transform.position.y,1);

            if (m_currentTime >= m_maxTime)
            {
                m_finalScale = m_initScale;
                m_initScale = m_currentScale;
                
                m_finalMove = m_initMove;
                m_initMove = m_currentMove;

                m_currentTime = 0;
            }
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        selector.enabled = true;
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
        selector.enabled = false;
    }
}
