using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    public GameObject SettingsUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
