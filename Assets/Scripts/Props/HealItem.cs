using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : MonoBehaviour
{
    public PlayerController playercontroller;
    public PlayerStats playerStats;
    public GameObject HPbar;
    [SerializeField] private float f_hp = 25;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Heal());
        }
    }

    //Heal
    public IEnumerator Heal()
    {
        playerStats.hp_stat = playerStats.hp_stat + f_hp;
        HPbar.SendMessage("TakeLife", f_hp);
        //Añadir Animacion Vida
        FindObjectOfType<AudioManager>().PlayRandomPitch("Heal");
        Destroy(gameObject);
        yield return new WaitForSeconds(1.0f);
    }
}
