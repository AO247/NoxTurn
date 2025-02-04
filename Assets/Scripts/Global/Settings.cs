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

    public InputActionReference pauseAction;

    public AudioMixer audioMixer;

    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    public Toggle fullScreenToggle;
    public GameObject firstSelected;
    public EventSystem eventSystem;

    private void Start()
    {
        // Na starcie ukrywamy menu pauzy
        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(true);

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        HashSet<string> uniqueResolutions = new HashSet<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            // Tworzymy string zawieraj¹cy tylko szerokoœæ i wysokoœæ
            string option = resolutions[i].width + " x " + resolutions[i].height;
            // Dodajemy tylko, jeœli taki rozmiar jeszcze nie zosta³ dodany
            if (uniqueResolutions.Add(option))
            {
                options.Add(option);
                // Jeœli aktualna rozdzielczoœæ ekranu odpowiada tej opcji,
                // zapamiêtujemy indeks (uwzglêdniamy, ¿e opcje mog¹ byæ filtrowane)
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
        // Wyszukujemy w³aœciw¹ rozdzielczoœæ na podstawie wybranego indeksu
        // UWAGA: W tym przypadku, po filtracji unikalnych rozdzielczoœci, mo¿e byæ trudniej
        // przypisaæ w³aœciwy element z tablicy resolutions. Mo¿esz przechowywaæ dodatkowo listê
        // dopasowanych Resolution lub wyszukaæ pierwsz¹, która pasuje do wybranej opcji.
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
