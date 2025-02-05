using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public GameObject island;
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
        } else
        {
            PlaySign.GetComponent<MeshRenderer>().material = Dark;
        }
        if (rot == 180) //SETTINGS
        {
            selectedOption = 1;
            //Debug.Log("90");
            SettingsSign.GetComponent<MeshRenderer>().material = Glow;
        } else
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

        if (Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("Fire3"))
        {
            if (selectedOption == 0)
            {
                PlayGame();
            }
            if (selectedOption == 2)
            {
                Debug.Log("quit");

                QuitGame();

            }
            if (selectedOption == 1)
            {
                Setting();
            }
            //else if(selectedOption == 3)
            //{
            //    if(pressed)
            //    {
            //        SelectLevel(selectedLevel);
            //    }
            //}
            pressed = true;

        }
        if (pressed && selectedOption == 3) {
            if(Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton4)) // Fire1 for LB
            {
                if(selectedLevel > 1)
                    selectedLevel--;
                Debug.Log("Selected Level: " + selectedLevel);
                //playerSound.PlayIn();
            }
            else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton5)) // Fire2 for RB
            {
                if (selectedLevel < 8)
                    selectedLevel++;
                Debug.Log("Selected Level: " + selectedLevel);
                //playerSound.PlayIn();
            }
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
        settingsUI.SetActive(true);
        //pauseMenuUI.SetActive(false);
        //settingsUI.GetComponent<Settings>().Select();
    }
}

