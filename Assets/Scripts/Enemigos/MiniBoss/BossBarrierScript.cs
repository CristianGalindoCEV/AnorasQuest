using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBarrierScript : MonoBehaviour
{
    [SerializeField] MiniBossScript miniBossHP;
   

    void Update()
    {
        if (miniBossHP.bossHP < 20)
        {
            transform.Translate(Vector3.down * Time.deltaTime);
        }
    }
}
