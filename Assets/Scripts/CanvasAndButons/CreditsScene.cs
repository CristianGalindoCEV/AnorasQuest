using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScene : MonoBehaviour
{
    public Transform text;
    private float f_push = -110;
    [SerializeField] private int i_speed = 30;
    [SerializeField]private float f_timer = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        f_push += Time.deltaTime * i_speed;

        text.position = new Vector3 (text.position.x, f_push, 0);

        f_timer += Time.deltaTime;

        if (f_timer >= 40)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
