using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
//using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour
{
    public PlayerStats PlayerStats;

    //UI Notification
    public Image Icon_L;
    public Image Icon_R;
    public Image Background_Icon;
    public TextMeshProUGUI ControlText;

    //Bufs UI Interface
    public Image Buff_Damage;
    public Image Buff_Cadence;
    public Image Buff_SpedBullet;

    //Audio
    public Slider musicSlider;
    public Slider mainVolumeSlider;
    public Slider soundSlider;
    public AudioMixer audioMixer;
    
    //UI Panels
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
        //UI Notification
        Icon_L.enabled = false;
        Icon_R.enabled = false;
        Background_Icon.enabled = false;

        //Menu UI
        panelgraphics.SetActive(false);
        panelresolution.SetActive(false);
        panelsound.SetActive(false);
        optionsmenu.SetActive(false);

        //UI Speed Buf
        if (PlayerStats.SpeedBulletBuf == false)
        {
            Buff_SpedBullet.enabled = false;
        }
        else
        {
            Buff_SpedBullet.enabled = true;
            Buff_SpedBullet.DOFade(1, 0f).SetEase(Ease.OutSine);
        }
       
        //UI Damage Buf
        if (PlayerStats.DamageBuf == false)
        {
            Buff_Damage.enabled = false;
        }
        else
        {
            Buff_Damage.enabled = true;
            Buff_Damage.DOFade(1, 0f).SetEase(Ease.OutSine);
        }
        
        //UI Cadende Buff
        if (PlayerStats.CadenceBuf == false)
        {
            Buff_Cadence.enabled = false;
        }
        else
        {
            Buff_Cadence.enabled = true;
            Buff_Cadence.DOFade(1, 0f).SetEase(Ease.OutSine);
        }

        //GetAudioOptions
        float MainVolume = PlayerPrefs.GetFloat("MainVol");
        mainVolumeSlider.value = MainVolume;

        float MusicVolume = PlayerPrefs.GetFloat("MusicVol");
        musicSlider.value = MusicVolume;

        float SoundVolume = PlayerPrefs.GetFloat("SoundVol");
        soundSlider.value = SoundVolume;

        //Resolution Options
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
        PlayerPrefs.SetFloat("MainVol", sliderValue);
        if (sliderValue == 0)
        {
            audioMixer.SetFloat("MainVolume", -60);
        }
    }
    
    public void SetVolumeMusic(float sliderValue)
    {
        audioMixer.SetFloat("MusicVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVol", sliderValue);
        
        if (sliderValue == 0)
        {
            audioMixer.SetFloat("MusicVol", -60);
        }
    }
   
    public void SetVolumeSound(float sliderValue)
    {
        audioMixer.SetFloat("SoundsVol", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SoundVol", sliderValue);
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

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions [resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
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

    //UI Bufs Animations
    public void UnlockedDamage()
    {
        Buff_Damage.enabled = true;
        Buff_Damage.DOFade(1, 1f).SetEase(Ease.OutSine);
    }
    public void UnlockedSpeed()
    {
        Buff_SpedBullet.enabled = true;
        Buff_SpedBullet.DOFade(1, 1f).SetEase(Ease.OutSine);
    }
    public void UnlockedCadence()
    {
        Buff_Cadence.enabled = true;
        Buff_Cadence.DOFade(1, 1f).SetEase(Ease.OutSine);
    }

    //Text Animation UI
    public void UiTextAnimation() // Animation when notificate ingame
    {
        //Reset Positions
        
        ControlText.DOFade(0f, 0f);
        Icon_L.enabled = false;
        Icon_R.enabled = false;
        Background_Icon.enabled = false;
        ControlText.enabled = false;

        Icon_L.rectTransform.localPosition = new Vector3(0, 0, 0);
        Icon_R.rectTransform.localPosition = new Vector3(0, 0, 0);
        Background_Icon.rectTransform.localScale = new Vector3(0, 1, 1);
        
        //Start
        
        Icon_L.enabled = true;
        Icon_R.enabled = true;
        Background_Icon.enabled = true;
        ControlText.enabled = true;

        //MeteAquiElAudio
        Icon_L.rectTransform.DOAnchorPosX(-450, 2f).SetEase(Ease.OutQuint);
        Icon_R.rectTransform.DOAnchorPosX(450, 2f).SetEase(Ease.OutQuint);
        Background_Icon.rectTransform.DOScaleX(0.45f, 2f).SetEase(Ease.OutQuint);

        ControlText.DOFade(1f, 2f).SetEase(Ease.OutQuint).SetDelay(1f);
    }
}
