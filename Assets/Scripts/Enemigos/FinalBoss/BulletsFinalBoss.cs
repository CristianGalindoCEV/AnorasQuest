using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletsFinalBoss : MonoBehaviour
{
    private float f_timeCounter = 0;
    private Transform m_player;
    private Vector3 playerVector;

    // Start is called before the first frame update
    void Start()
    {
        m_player = GameObject.FindGameObjectWithTag("Player").transform;
        playerVector = m_player.position;
    }

    // Update is called once per frame
    void Update()
    {
        f_timeCounter += Time.deltaTime;
        transform.LookAt(playerVector);
        //transform.localScale = new Vector3 (1,1,10);
        transform.DOScale(new Vector3 (1,1,150), 1f).SetDelay(1.1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(DestroyObject());
        }
        if (other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);
    }
}
