using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallAttack : MonoBehaviour
{
    private GameObject m_player;
    private GameObject m_Me;
    private Vector3 m_playerPos;
    // Start is called before the first frame update
    void Start()
    {
        m_Me = this.gameObject;
        transform.localScale = new Vector3(0,0,0);
        m_player = GameObject.Find("Player");
        StartCoroutine(Go());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(m_Me);
        }
    }

    private void Completed()
    {
        StartCoroutine(Destroy());
    }
    IEnumerator Go()
    {
        transform.DOScale(2, 2f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(2f);
        m_playerPos = m_player.transform.position;
        transform.DOMove(m_playerPos, 1f).SetEase(Ease.InCubic).OnComplete(Completed);
    }
    IEnumerator Destroy()
    {
        Destroy(m_Me);
        yield return new WaitForSeconds(0f);
    }
}
