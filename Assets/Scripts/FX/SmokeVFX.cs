using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SmokeVFX : MonoBehaviour
{
    public VisualEffect vfxSmoke;
    void Start()
    {
        StartCoroutine(OnOff());
    }
    public IEnumerator OnOff()
    {
        vfxSmoke.SendEvent("PlaySmoke");
        yield return new WaitForSeconds(3.0f);
        vfxSmoke.SendEvent("StopSmoke");
        yield return new WaitForSeconds(1.0f);

    }
}
