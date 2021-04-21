using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class StartMenuInteractions : MonoBehaviour
{
    [SerializeField]
    AudioMixer AM;

    public void StartGame()
    {
        GameManager.Instance.GameStart();
    }

    public void LoadMode(string modeName)
    {
        GameManager.Instance.LoadScene(modeName);
    }

    public void ChangeLanguage(string language)
    {
        PlayerPrefs.SetString("language", language);
        Language[] texts =  FindObjectsOfType<Language>();

        foreach (Language text in texts)
        {
            text.ChangeLanguage();
        }
    }

    public void ModifyMasterVolume(Slider slider)
    {
        AM.SetFloat("MasterVolume", slider.value);
    }

    public void ModifyMusicVolume(Slider slider)
    {
        AM.SetFloat("MusicVolume", slider.value);
    }

    public void ModifySFXVolume(Slider slider)
    {
        AM.SetFloat("SFXVolume", slider.value);
    }
}
