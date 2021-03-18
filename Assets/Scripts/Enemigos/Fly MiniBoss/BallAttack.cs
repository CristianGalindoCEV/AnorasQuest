using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BallAttack : MonoBehaviour
{
    private GameObject m_player;
    private Vector3 m_playerPos;
    private float speed = 7f;
    // Start is called before the first frame update
    void Start()
    {
        transform.localScale = new Vector3(0,0,0);
        m_player = GameObject.Find("Player");
        StartCoroutine(Go());
    }

    private void Destroy()
    {
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy();
        }
    }
    IEnumerator Go()
    {
        transform.DOScale(2, 2f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(2f);
        m_playerPos = m_player.transform.position;

        transform.DOMove(m_playerPos, 1f).SetEase(Ease.InCubic).OnComplete(Destroy);
    }
}
