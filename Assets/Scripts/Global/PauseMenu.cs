using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public InputActionReference pauseAction;
    public Button resumeButton;
    public Button settingsButton;
    public Button quitButton;

    private void Start()
    {
        // Na starcie ukrywamy menu pauzy
        pauseMenuUI.SetActive(false);
    }

    private void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenuUI.SetActive(isPaused);

        // Wstrzymanie lub wznowienie gry
        Time.timeScale = isPaused ? 0f : 1f;
    }
    private void Update()
    {
        if (pauseAction.action.triggered)
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
}
