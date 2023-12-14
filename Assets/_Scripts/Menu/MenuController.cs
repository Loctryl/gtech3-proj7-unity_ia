using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.LookDev;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField]
    private TMP_Text volumeTextValue = null;
    [SerializeField]
    private Slider volumeSlider = null;
    [SerializeField]
    private float defaultVolume = 1.0f;

    [Header("Gameplay Settings")]
    [SerializeField]
    private TMP_Text ControllerSenTextValue = null;
    [SerializeField]
    private Slider ControllerSenSlider = null;
    [SerializeField]
    private float defaultControllerSen = 4;
    public int mainControllerSen = 4;

    [Header("Graphics Settings")]
    [SerializeField]
    private TMP_Text BrightnessTextValue = null;
    [SerializeField]
    private Slider BrightnessSlider = null;
    [SerializeField]
    private float defaultBrightness = 1;

    [Space(10)]
    [SerializeField]
    private TMP_Dropdown qualityDropdown;
    [SerializeField]
    private Toggle fullscreenToggle;

    private int _qualityLevel;
    private bool _isFullscreen;
    private float _brightnessLevel;

    [Header("Confirmation Prompt")]
    [SerializeField]
    private GameObject ConfirmationPrompt = null;

    [Header("Level To Load")]
    public string nameGameLevel;
    private string LevelToLoad;
    [SerializeField]
    private GameObject noSavedGameMenu = null;

    [Header("Resolution Dropdown")]
    public TMP_Dropdown resolutionDropdown;
    private Resolution[] resolutions;

    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionsIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionsIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionsIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }
    public void NewGameYes()
    {
        SceneManager.LoadScene(nameGameLevel);
    }

    public void LoadGameYes()
    {
        if (PlayerPrefs.HasKey("SavedLevel"))
        {
            LevelToLoad = PlayerPrefs.GetString("SavedLevel");
            SceneManager.LoadScene(LevelToLoad);
        }
        else
        {
            noSavedGameMenu.SetActive(true);
        }
    }

    public void ExitMenu()
    {
        Application.Quit();
    }   

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        volumeTextValue.text = volume.ToString("0.0");
    }

    public void VolumeApply()
    {
        PlayerPrefs.SetFloat("Master Volume", AudioListener.volume);
        StartCoroutine(ConfirmationBox());
    }

    public void SetControllerSen(float controllerSen)
    {
        mainControllerSen = Mathf.RoundToInt(controllerSen);
        ControllerSenTextValue.text = mainControllerSen.ToString("0");
    }

    public void GameplayApply()
    {
        PlayerPrefs.SetInt("Master Sensitivity", mainControllerSen);
        StartCoroutine(ConfirmationBox());
    }

    public void SetBrightness(float brightness)
    {
        _brightnessLevel = brightness;
        BrightnessTextValue.text = brightness.ToString("0.0");
    }   

    public void setFullscrren(bool isFullscreen)
    {
        _isFullscreen = isFullscreen;
    }

    public void setQuality(int qualityIndex)
    {
        _qualityLevel = qualityIndex;
    }

    public void GraphicsApply()
    {
        QualitySettings.SetQualityLevel(_qualityLevel);
        Screen.fullScreen = _isFullscreen;
        RenderSettings.ambientIntensity = _brightnessLevel;
        PlayerPrefs.SetInt("Quality Level", _qualityLevel);
        PlayerPrefs.SetInt("Fullscreen", _isFullscreen ? 1 : 0);
        PlayerPrefs.SetFloat("Brightness", _brightnessLevel);
        StartCoroutine(ConfirmationBox());
    }
    public void VolumeReset(string menuType)
    {
        if (menuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
        if (menuType == "Gameplay")
        {
            mainControllerSen = Mathf.RoundToInt(defaultControllerSen);
            ControllerSenSlider.value = defaultControllerSen;
            ControllerSenTextValue.text = defaultControllerSen.ToString("0");
            GameplayApply();    
        }
        if (menuType == "Graphics")
        {
            BrightnessSlider.value = defaultBrightness;
            BrightnessTextValue.text = defaultBrightness.ToString("0.0");

            qualityDropdown.value = 1;
            QualitySettings.SetQualityLevel(1);

            fullscreenToggle.isOn = true;
            Screen.fullScreen = false;

            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, Screen.fullScreen);
            resolutionDropdown.value = resolutions.Length;

            GraphicsApply();
        }
    }
    public IEnumerator ConfirmationBox()
    {
        ConfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ConfirmationPrompt.SetActive(false);
    }
    
}
