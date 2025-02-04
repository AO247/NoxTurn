using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine.EventSystems;

public class Settings : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsUI;
    public GameObject graphicsSettings;
    public GameObject controlsSettings;
    public GameObject canvas;
    public UnityEngine.UI.Button graphicsButton;
    public UnityEngine.UI.Button controlsButton;
    public UnityEngine.UI.Button backButton;

    public InputActionReference pauseAction;

    public AudioMixer audioMixer;

    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    public GameObject firstSelected;
    public EventSystem eventSystem;

    public Slider sfxSlider;



    private void Start()
    {
        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(true);

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        HashSet<string> uniqueResolutions = new HashSet<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            if (uniqueResolutions.Add(option))
            {
                options.Add(option);

                if (resolutions[i].width == Screen.currentResolution.width &&
                    resolutions[i].height == Screen.currentResolution.height)
                {
                    currentResolutionIndex = options.Count - 1;
                }
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        fullScreenToggle.isOn = Screen.fullScreen;
    }

    private void Update()
    {
        if (pauseAction.action.triggered && PauseMenu.isPaused)
        {
            Debug.Log("Settings" + settingsUI.activeSelf);
            Back();
        }
    }

    public void SetResolution(int resolutionIndex)
    {
      
        string selectedOption = resolutionDropdown.options[resolutionIndex].text;
        foreach (Resolution res in resolutions)
        {
            string option = res.width + " x " + res.height;
            if (option == selectedOption)
            {
                Screen.SetResolution(res.width, res.height, Screen.fullScreen);
                break;
            }
        }
    }

    public void SetMaster(float volume)
    {
        Debug.Log(audioMixer.GetFloat("Volume", out volume));
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetMusic(float volume)
    {
        audioMixer.SetFloat("Music", volume);
    }

    public void SetSFX(float volume)
    {
        audioMixer.SetFloat("SFX", volume);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void Graphics()
    {
        graphicsSettings.SetActive(true);
        controlsSettings.SetActive(false);
        Navigation nav = graphicsButton.navigation;
        nav.selectOnDown = fullScreenToggle;
        graphicsButton.navigation = nav;
        
        nav = controlsButton.navigation;
        nav.selectOnDown = fullScreenToggle;
        controlsButton.navigation = nav;

        nav = backButton.navigation;
        nav.selectOnUp = sfxSlider;
        backButton.navigation = nav;

    }

    public void Controls()
    {
        controlsSettings.SetActive(true);
        graphicsSettings.SetActive(false);
    }

    public void Save()
    {
        // Zapis ustawieñ
    }

    public void Back()
    {
        settingsUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        canvas.GetComponent<PauseMenu>().Select();
    }

    public void Select()
    {
        eventSystem.SetSelectedGameObject(firstSelected);
    }
}
