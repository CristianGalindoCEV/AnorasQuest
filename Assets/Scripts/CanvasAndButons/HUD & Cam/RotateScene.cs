using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScene : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float speed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0f, speed, 0));

        if (speed == 0)
        {
            Debug.Log("No speed");
        }
    }
}
