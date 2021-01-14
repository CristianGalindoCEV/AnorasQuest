using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyMiniBoss : MonoBehaviour
{
    
    private bool b_startFight = false;
    
    //SpacePoints
    public SpacePoint [] points;
    private int i_currentPoint = 0;

    //Boss
    [SerializeField] private float f_speed = 3;
    Transform my_transform;
    public GameObject insect;
    public Collider m_triger;

    //Player
    public Transform player;

    //Easing
    private float f_currentTime = 0;
    private float f_initValue;
    private float f_finalValue;
    private float f_maxTime = 2f;

    // Start is called before the first frame update
    void Start()
    {
       
        my_transform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        f_currentTime += Time.deltaTime;

        if (b_startFight == true) 
        {
            /*Vector3 loockAtPosition = player.position;
            loockAtPosition.x = transform.position.x;
            transform.LookAt(loockAtPosition);
            */

            if (Vector3.Distance(transform.position, points[i_currentPoint].transform.position) < 0.2f) //Miramos si hemos llegado al punto
            {
                StartCoroutine(StopMove());
                i_currentPoint++;
                i_currentPoint %= points.Length;
            }
            else // Pasamos al siguiente punto
            {
                transform.position = Vector3.MoveTowards(transform.position, points[i_currentPoint].transform.position, Time.deltaTime * f_speed);
                Debug.Log("meMuevo");
            }

            if (f_currentTime == 10f)
            {
                StartCoroutine(FirtsAttack());
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && b_startFight == false)
        {
            b_startFight = true;

            m_triger.enabled = false;
            
            //transform.position = Vector3.MoveTowards(transform.position, points[i_currentPoint].transform.position, Time.deltaTime * f_speed);
            Easing.CircEaseOut(f_currentTime, f_initValue, f_finalValue - f_initValue, f_maxTime);
        }
    }

    IEnumerator FirtsAttack()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 bulletPosition = (transform.position);
            bulletPosition.y = Random.Range(3,10);
            Instantiate(insect, bulletPosition, transform.rotation);
        }
       
        yield return new WaitForSeconds(0f);
    }
    IEnumerator StopMove()
    {
        float f_stop;
        
        f_stop = Random.Range(1f, 2.5f);
        f_speed = 0;
        
        //Idle Aimation
        
        yield return new WaitForSeconds(f_stop);
        f_speed = 6f;
    }
}
