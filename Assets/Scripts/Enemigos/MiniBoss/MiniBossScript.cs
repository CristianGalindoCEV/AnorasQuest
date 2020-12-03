using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniBossScript : MonoBehaviour
{
    private float TimeCounter = 0;
    [SerializeField] GameObject spikePrefab;
    [SerializeField] GameObject spikeCagePrefab;
    [SerializeField] Transform player;

    private int randomNumber;
    public float damage;
    bool isDead = false;
    public GameMaster gamemaster;
    private GameObject m_boss;

    public MinibossHP minibosshp;

    void Start()
    {
        m_boss = GameObject.Find("Miniboss_Static");
    }
    void Update()
    {
        TimeCounter += Time.deltaTime;
        //Ataques
        if (TimeCounter > 3 && minibosshp.hp > 0)
        {
            TimeCounter = 0;
            randomNumber = Random.Range (1,10);

            if (randomNumber < 6) { SpikeAttack(); /*Debug.Log("SpikeAttack")*/;}
            else {SpikeCage(); /*Debug.Log("SpikeCage")*/; }
        }

        if( minibosshp.hp <= 0)
        {
            m_boss.SetActive(false);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
    
        if ( other.tag  == "Bullet")
        {
            StartCoroutine(Damage());
            damage = gamemaster.bulletDamage;
        }

    }

    void SpikeAttack ()
    {
        Instantiate(spikePrefab, new Vector3(player.transform.position.x, 0, player.transform.position.z), transform.rotation);
    }

    void SpikeCage ()
    {
        Instantiate(spikeCagePrefab, new Vector3(player.transform.position.x, 0, player.transform.position.z), transform.rotation);
    }

    IEnumerator Damage()
    {
        minibosshp.hp = minibosshp.hp - damage;
        yield return new WaitForSeconds(1.0f);
    }

}
