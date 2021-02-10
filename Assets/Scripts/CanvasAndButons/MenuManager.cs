using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject optionsmenu;
    public GameObject panelresolution;
    public GameObject panelsound;
    public GameObject panelgraphics;
    public TMP_Dropdown resolutionDropdown;
    public InputManager inputmanager;
    public PauseManager pausemanager;
    Resolution[] resolutions;

    void Start()
    {
        panelgraphics.SetActive(false);
        panelresolution.SetActive(false);
        panelsound.SetActive(false);
        optionsmenu.SetActive(false);
        
        

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

    void Update()
    {
        if( inputmanager.menuon == false)
        {
            panelgraphics.SetActive(false);
            panelresolution.SetActive(false);
            panelsound.SetActive(false);
        }
    }

    //Volumenes de audio
    public void SetVolume(float sliderValue)
    {
        audioMixer.SetFloat("MainVolume", Mathf.Log10(sliderValue) * 20);
        if (sliderValue == 0)
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
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    //Menu pause buttons

    public void PulsaResume()
    {
        inputmanager.pausemenu.SetActive(false);
        inputmanager.menuon = false;
        pausemanager.Resume();
        FindObjectOfType<AudioManager>().Play("Button");
    }

    public void PulsaOptions()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        optionsmenu.SetActive(true);
        inputmanager.pausemenu.SetActive(false);
    }

    public void PulsaExit()
    {
        SceneManager.LoadScene("MainMenu");
    }
   
    //Botones del menu Ingame
    public void PulsaResolution()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        panelgraphics.SetActive(false);
        panelresolution.SetActive(true);
        panelsound.SetActive(false);
    }

    public void PulsaSound()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        panelgraphics.SetActive(false);
        panelresolution.SetActive(false);
        panelsound.SetActive(true);
    }

    public void PulsaGraphics()
    {
        FindObjectOfType<AudioManager>().Play("Button");
        panelgraphics.SetActive(true);
        panelresolution.SetActive(false);
        panelsound.SetActive(false);
    }
}
