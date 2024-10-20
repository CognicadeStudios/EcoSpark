using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject SettingsUI;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("Volume", 0.5f);
        PlayerPrefs.SetString("MayorName", "Arush");
    }

    public void ToggleSettingsMenu()
    {
        if (SettingsUI.activeSelf)
        {
            CloseSettingsMenu();
        }
        else
        {
            OpenSettingsMenu();
        }
    }

    public void OpenSettingsMenu()
    {
        SettingsUI.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        SettingsUI.SetActive(false);
    }
    
    public TMP_InputField mayorNameText;
    public Slider volumeSlider;
    public void UpdatePrefs()
    {
        PlayerPrefs.SetFloat("Volume", volumeSlider.value);
        PlayerPrefs.SetString("MayorName", mayorNameText.text);

        SoundManager.instance.audioSource.volume = volumeSlider.value;
    }

    public TMP_InputField seedTextInput;
    public void UpdateSeed()
    {
        if(seedTextInput.text == "")
        {
            if(PlayerPrefs.HasKey("Seed"))PlayerPrefs.DeleteKey("Seed");
        }
        else 
        {
            PlayerPrefs.SetInt("Seed", int.Parse(seedTextInput.text));
        }
    }
}
