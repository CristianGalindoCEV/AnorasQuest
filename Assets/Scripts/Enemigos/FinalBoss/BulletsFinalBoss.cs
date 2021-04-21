using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletsFinalBoss : MonoBehaviour
{
    private float f_timeCounter = 0;
    private Transform m_player;
    public Vector3 playerVector;
    public GameObject fire;
    public Collider mycollider;

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
        fire.transform.LookAt(playerVector);
        //Scale Object 
        mycollider.transform.DOScale(new Vector3 (1,1,50), 1f).OnComplete(DestroyThisObject);
    }

    private void DestroyThisObject ()
    {
        StartCoroutine(DestroyObject());
    }
    IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        Destroy(fire);
    }
}
