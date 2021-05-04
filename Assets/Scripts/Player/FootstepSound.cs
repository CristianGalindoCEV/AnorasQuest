using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FootstepSound : MonoBehaviour
{
    VisualEffect vfxSmoke;
    void Start()
    {
        vfxSmoke = GetComponent<VisualEffect>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            FindObjectOfType<AudioManager>().PlayRandomPitch("Footstep");
            vfxSmoke.SendEvent("FootstepPlay");
        }
    }
}
