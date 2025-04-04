using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    public GameObject ContinueSign;
    public GameObject NoxTurn;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

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
        if (rot == 0) //QUIT
        {
            selectedOption = 3;
            ContinueSign.GetComponent<MeshRenderer>().material = Glow;
            NoxTurn.GetComponent<MeshRenderer>().material = Glow;

        }
        else
        {
            ContinueSign.GetComponent<MeshRenderer>().material = Dark;
            NoxTurn.GetComponent<MeshRenderer>().material = Dark;


        }

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
        saveSliders(new SaveData());
        StartCoroutine(_LoadScene(SceneManager.GetActiveScene().buildIndex + 1));
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void saveSliders(SaveData saveData)
    {
        saveData.masterVolume = masterSlider.value;
        saveData.musicVolume = musicSlider.value;
        saveData.sfxVolume = sfxSlider.value;
        SaveManager.SaveGameState(saveData);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Setting()
    {
        Floor.isPaused = true;
        settingsUI.SetActive(true);
        settingsUI.GetComponent<Settings>().Select();
        //pauseMenuUI.SetActive(false);
        //settingsUI.GetComponent<Settings>().Select();
    }
    public void Continue()
    {
        SaveData saveData = SaveManager.LoadGameState();
        if (saveData != null)
        {
            saveSliders(saveData);
            StartCoroutine(_LoadScene(saveData.lvlNumber));
        }
        else
        {
            saveSliders(new SaveData());
            StartCoroutine(_LoadScene(SceneManager.GetActiveScene().buildIndex + selectedLevel));
        }
    }
}

