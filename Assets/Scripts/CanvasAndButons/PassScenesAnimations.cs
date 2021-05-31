using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassScenesAnimations : MonoBehaviour
{
    public float TimeToTransition;
    [SerializeField] private float f_currentime;

    //Put your current Scene true
    public bool Animation_1;
    public bool Animation_2;
    public bool Animation_3;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Animation_1 == false && Animation_2 == false && Animation_3 == false)
        {
            Debug.Log("Put One Bool true");
        }
    }

    // Update is called once per frame
    void Update()
    {
        f_currentime += Time.deltaTime;
        if (f_currentime >= TimeToTransition)
        {
            NextScene();
        }
    }

    //Chose only 1 bool true to change scene
    private void NextScene() 
    {
        if (Animation_1 == true)
        {
            SceneManager.LoadScene("Intro2");
        }
        if (Animation_2 == true)
        {
            SceneManager.LoadScene("Intro3");
        }
        if (Animation_3 == true)
        {
            SceneManager.LoadScene("BigLevel");
        }
    }
}
