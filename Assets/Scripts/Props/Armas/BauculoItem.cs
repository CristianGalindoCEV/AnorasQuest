using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class BauculoItem : MonoBehaviour
{
    [SerializeField] private int m_speed = 30;
    public GameMaster gameMaster;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, m_speed * Time.deltaTime);
    }

    //triggers
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //Decirle al imput manager que desbloqueado es true
            StartCoroutine(unlocked());
        }
    }

    IEnumerator unlocked()
    {
        //Particula
        //Sonido
        gameMaster.UnlockWeapon();
        yield return new WaitForSeconds(0.5f);
        Destroy (gameObject);
    }
}
