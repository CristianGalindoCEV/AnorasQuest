using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonEasing : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image selector;
    private float m_initMove;
    private float m_finalMove;
    private float m_currentMove;

    public RectTransform rectTransform;
    [SerializeField]private Vector3 m_startPosition;
    private float m_duration = 1f;

    private float m_currentTime;
    private float m_maxTime = 1;
    [SerializeField]private bool myanimation = false;

    // Start is called before the first frame update
    void Start()
    {
        m_initMove = selector.transform.position.x - 10;
        m_finalMove = selector.transform.position.x + 3;
        m_currentMove = m_initMove;
        selector.enabled = false;
        
        m_startPosition = rectTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(myanimation == true)
        {
            m_currentTime += Time.deltaTime;
            
            m_currentMove = Easing.CubicEaseInOut(m_currentTime, m_initMove, m_finalMove - m_initMove, m_maxTime);
            selector.transform.position = new Vector3(m_currentMove, selector.transform.position.y,1);
            
            //rectTransform.DOMoveX(0.5f, m_duration).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo);

            if (m_currentTime >= m_maxTime)
            {
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
       // rectTransform.localPosition = m_startPosition;
        myanimation = false;
        m_currentTime = 0;
        selector.enabled = false;
    }
}
