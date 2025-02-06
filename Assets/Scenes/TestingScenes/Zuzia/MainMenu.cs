using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject island;
    [SerializeField] private GlobalSound globalSound;
    public InputActionReference interaction;

    //SceneFade sceneFade;
    int selectedOption = 0, selectedLevel = -1;
    bool pressed = false;
    public Material Dark;
    public Material Glow;
    //AudioManager audioManager;

    //[SerializeField] private PlayerSound playerSound;
    public GameObject settingsUI;

    public GameObject PlaySign;
    public GameObject SettingsSign;
    public GameObject QuitSign;

    private void Awake()
    {
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        settingsUI.SetActive(false);

        //sceneFade = Object.FindFirstObjectByType<SceneFade>();
    }
    private void Update()
    {
        float rot = island.transform.rotation.eulerAngles.y;
        Debug.Log(rot);
        if (rot == 90) //PLAY
        {
            selectedOption = 0;
            //Debug.Log("-90");
            PlaySign.GetComponent<MeshRenderer>().material = Glow;
        }
        else
        {
            PlaySign.GetComponent<MeshRenderer>().material = Dark;
        }
        if (rot == 180) //SETTINGS
        {
            selectedOption = 1;
            //Debug.Log("90");
            SettingsSign.GetComponent<MeshRenderer>().material = Glow;
        }
        else
        {
            SettingsSign.GetComponent<MeshRenderer>().material = Dark;
        }

        if (rot == 270) //QUIT
        {
            selectedOption = 2;
            //Debug.Log("180");
            QuitSign.GetComponent<MeshRenderer>().material = Glow;
        }
        else
        {
            QuitSign.GetComponent<MeshRenderer>().material = Dark;
        }
        //else if (rot == 270)
        //{
        //    selectedOption = 3;
        //    Debug.Log("270");
        //}

        if (interaction.action.triggered && !Floor.isPaused)
        {
            if (selectedOption == 0)
            {
                globalSound.PlayTurn();
                PlayGame();
            }
            if (selectedOption == 1)
            {
                globalSound.PlayTurn();

                Setting();
            }
            if (selectedOption == 2)
            {
                Debug.Log("quit");
                globalSound.PlayTurn();
                QuitGame();

            }
            else if (selectedOption == 3)
            {
                globalSound.PlayTurn();
                Continue();
            }
            pressed = true;

        }
    }
    public IEnumerator _LoadScene(int sceneIndex)
    {
        //sceneFade.FadeIn();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneIndex);
    }
    public void PlayGame()
    {
        StartCoroutine(_LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void SelectLevel(int selectedLevel)
    {
        StartCoroutine(_LoadScene(SceneManager.GetActiveScene().buildIndex + selectedLevel));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Setting()
    {
        Floor.isPaused = true;
        settingsUI.SetActive(true);
        //pauseMenuUI.SetActive(false);
        //settingsUI.GetComponent<Settings>().Select();
    }
    public void Continue()
    {
        SaveData saveData = SaveManager.LoadGameState();
        if (saveData != null)
        {
            StartCoroutine(_LoadScene(saveData.lvlNumber));
        }
        else
        {
            StartCoroutine(_LoadScene(SceneManager.GetActiveScene().buildIndex + selectedLevel));
        }
    }
}

