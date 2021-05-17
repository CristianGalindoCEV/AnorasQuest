using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using Cinemachine;


public class PortalButton : MonoBehaviour
{
    //Controlers
    public PlayableDirector myAnimation;
    public PlayerController playerController;
    public InputManager inputManager;
    public CinemachineVirtualCamera animatica_cam;

    //Portal
    public Transform wallPortal;
    private bool pulse = false;

    //Easing
    float currentTime = 0;
    float initValue;
    float finalValue;
    float currentValue;
    float maxTime = 5f;
    private Vector3 upPortal;

    float button_initValue;
    float button_finalValue;
    float button_currentValue;
    float button_maxTime = 2f;

   // public VisualEffect vfxSmoke;
    public GameObject vfxSmoke;

    // Start is called before the first frame update
    void Start()
    {
        animatica_cam.enabled = false;
        upPortal = wallPortal.transform.position;

        initValue = upPortal.y;
        finalValue = upPortal.y - 15;
        currentValue = initValue;

        button_currentValue = button_initValue;
        button_finalValue = transform.position.y - 5;
        button_initValue = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if(pulse == true)
        {
            currentTime += Time.deltaTime;

            if(currentTime <= maxTime)
            {
                currentValue = Easing.CubicEaseIn(currentTime, initValue, finalValue - initValue, maxTime);
            }
            if (currentTime <= button_maxTime)
            {
                button_currentValue = Easing.CubicEaseIn(currentTime, button_initValue, button_finalValue - button_initValue, button_maxTime);
            }
            wallPortal.position = new Vector3(wallPortal.position.x, currentValue, wallPortal.position.z);
            transform.position = new Vector3(transform.position.x, button_currentValue, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && pulse == false)
        {
            pulse = true;
            FindObjectOfType<AudioManager>().Play("UnlockPortal");
            StartCoroutine(Destroy());
            StartCoroutine(Cinematic());
        }
    }
    IEnumerator Cinematic()
    {
        playerController.speed = 0f; // Dont move 
        inputManager.animationPlayed = true;

        myAnimation.Play(); // Start animatic
        animatica_cam.enabled = true;
        
        yield return new WaitForSeconds(6f);

        animatica_cam.enabled = false;
        myAnimation.Stop();//Finish

        yield return new WaitForSeconds(2f);
        playerController.speed = 10f; // Move
        inputManager.animationPlayed = false;
    }
    public IEnumerator Destroy()
    {
        Instantiate(vfxSmoke, transform.position, transform.rotation);
        yield return new WaitForSeconds(9f);
        Destroy(gameObject); 
    }
}
