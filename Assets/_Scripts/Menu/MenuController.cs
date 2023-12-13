using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    private GameObject ConfirmationPrompt = null;

    [Header("Level To Load")]
    public string nameGameLevel;
    private string LevelToLoad;
    [SerializeField]
    private GameObject noSavedGameMenu = null;

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

    public void VolumeReset(string menuType)
    {
        if (menuType == "Audio")
        {
            AudioListener.volume = defaultVolume;
            volumeSlider.value = defaultVolume;
            volumeTextValue.text = defaultVolume.ToString("0.0");
            VolumeApply();
        }
    }
    public IEnumerator ConfirmationBox()
    {
        ConfirmationPrompt.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ConfirmationPrompt.SetActive(false);
    }
    
}
