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

    public MinibossHP minibosshp;

    void Start()
    {
        
    }
    void Update()
    {
        TimeCounter += Time.deltaTime;
        //Ataques
        if (TimeCounter > 3)
        {
            TimeCounter = 0;
            randomNumber = Random.Range (1,10);

            if (randomNumber < 6) { SpikeAttack(); /*Debug.Log("SpikeAttack")*/;}
            else {SpikeCage(); /*Debug.Log("SpikeCage")*/; }
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
