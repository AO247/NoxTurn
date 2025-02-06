using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject settingsUI;
    public InputActionReference pauseAction;
    public GameObject firstSelected;
    public EventSystem eventSystem;

    private void Start()
    {
        // Na starcie ukrywamy menu pauzy
        pauseMenuUI.SetActive(false);
        settingsUI.SetActive(false);
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);
        Select();

        // Wstrzymanie lub wznowienie gry
        Time.timeScale = isPaused ? 0f : 1f;
    }
    private void Update()
    {
        if (pauseAction.action.triggered && settingsUI.activeSelf == false)
        {
            TogglePause();
        }
    }
    public void Resume()
    {
        if (isPaused)
        {
            TogglePause();
        }
        
    }
    public void Setting()
    {
        settingsUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        settingsUI.GetComponent<Settings>().Select();
    }
    public void MainMenu()
    {
        TogglePause();
        SceneManager.LoadScene(0);
    }
    public void Select()
    {
        eventSystem.SetSelectedGameObject(firstSelected);
    }
}
