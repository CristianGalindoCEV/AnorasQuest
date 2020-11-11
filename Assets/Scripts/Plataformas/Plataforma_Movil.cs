using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plataforma_Movil : MonoBehaviour
{
    public Rigidbody platformPoint;
    public Transform platform;
    public Transform[] platformPositions;
    public float platformSpeed;

    private int acutalPosition = 0;
    private int nextPosition = 1;

    public bool moveToNext = true;
    public float waitTime;

    void Update()
    {
        MovePlatform();
    }

    void MovePlatform()
    {
        if (moveToNext)
        {
            StopCoroutine(WaitForMove(0));
            platform.position = Vector3.MoveTowards(platform.position, platformPositions[nextPosition].position, platformSpeed * Time.deltaTime);
        }
      
        if (Vector3.Distance(platform.position, platformPositions[nextPosition].position) <= 0)
        {
            StartCoroutine(WaitForMove(waitTime));
            acutalPosition = nextPosition;
            nextPosition++;

            if (nextPosition > platformPositions.Length - 1)
            {
                nextPosition = 0;
            }
        }
    }
    IEnumerator WaitForMove(float time)
    {
        moveToNext = false;
        yield return new WaitForSeconds(time);
        moveToNext = true;
    }

}
