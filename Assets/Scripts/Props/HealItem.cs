using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    public PlayerController playercontroller;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playercontroller.HealItem();
            Destroy(gameObject);
        }
    }
}
