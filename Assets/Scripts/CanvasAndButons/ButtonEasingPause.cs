using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonEasingPause : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image selector;
    private PauseManager m_pauseManager;
    public RectTransform rectTransform;
    [SerializeField] private Vector3 m_startPosition;
    private float m_duration = 1f;
    Tween coso;

    [SerializeField] private bool myanimation = false;

    // Start is called before the first frame update
    void Start()
    {
        selector.enabled = false;
        m_pauseManager = FindObjectOfType<PauseManager>();
        m_startPosition = rectTransform.position;
    }

    private void OnDisable()
    {
        coso.Kill();
        rectTransform.position = m_startPosition;
        myanimation = false;
        selector.enabled = false;
    }

    private void Update()
    {
        if (selector.enabled == false)
        {
            coso.Kill();
            rectTransform.position = m_startPosition;
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        selector.enabled = true;
        coso = rectTransform.DOAnchorPosX(-70, m_duration).SetEase(Ease.InOutCubic).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        coso.Kill();
        rectTransform.position = m_startPosition;
        myanimation = false;
        selector.enabled = false;
    }
}
