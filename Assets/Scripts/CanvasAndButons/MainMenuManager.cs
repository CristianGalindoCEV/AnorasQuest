using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
//using UnityEngine.UIElements;

public class MainMenuManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject panelresolution;
    public GameObject panelsound;
    public GameObject panelgraphics;
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    [Range(0f,255f)]
    public float GammaCorrection;

    public Slider sliderRef;

    void Start()
    {
        panelgraphics.SetActive(false);
        panelresolution.SetActive(false);
        panelsound.SetActive(false);

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i= 0; i< resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && 
                resolutions [i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
 
    //Sound Voids
    public void SetVolume (float sliderValue)
    {
        audioMixer.SetFloat("MainVolume", Mathf.Log10 (sliderValue) * 20);
        if(sliderValue == 0)
        {
            audioMixer.SetFloat("MainVolume", -60);
        }
    }
    public void SetVolumeMusic(float sliderValue)
    {
        audioMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        if (sliderValue == 0)
        {
            audioMixer.SetFloat("MusicVol", -60);
        }
    }
    public void SetVolumeSound(float sliderValue)
    {
        audioMixer.SetFloat("SoundsVol", Mathf.Log10(sliderValue) * 20);
        if (sliderValue == 0)
        {
            audioMixer.SetFloat("SoundsVol", -60);
        }
    }
   
    //Botones de los settings
    public void SetQuality (int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        //QualitySettings.antiAliasing
    }
    public void SetGamma(float sliderValue)
    {
        GammaCorrection = sliderValue;
        RenderSettings.ambientLight = new Color(GammaCorrection, GammaCorrection, GammaCorrection, 1.0f);
        RenderSettings.ambientIntensity = sliderValue;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    //Botones del menu Ingame
    public void PulsaResolution()
    {
        panelgraphics.SetActive(false);
        panelresolution.SetActive(true);
        panelsound.SetActive(false);
        FindObjectOfType<AudioManager>().PlayRandomPitch("Button");
    }

    public void PulsaSound()
    {
        panelgraphics.SetActive(false);
        panelresolution.SetActive(false);
        panelsound.SetActive(true);
        FindObjectOfType<AudioManager>().PlayRandomPitch("Button");
    }

    public void PulsaGraphics()
    {
        panelgraphics.SetActive(true);
        panelresolution.SetActive(false);
        panelsound.SetActive(false);
        FindObjectOfType<AudioManager>().PlayRandomPitch("Button");
    }
   
}

