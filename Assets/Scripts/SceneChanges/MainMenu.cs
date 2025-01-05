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
    private void Start()
    {
        //sceneFade = Object.FindFirstObjectByType<SceneFade>();
    }
    private void Update()
    {
        float rot = island.transform.rotation.eulerAngles.y;
        if (rot == 0) //PLAY
        {
            selectedOption = 0;
            //Debug.Log("0");
        }
        else if (rot == 90) //SETTINGS
        {
            selectedOption = 1;
            //Debug.Log("90");
        }
        else if (rot == 180) //QUIT
        {
            selectedOption = 2;
            //Debug.Log("180");
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
            }
            else if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton5)) // Fire2 for RB
            {
                if (selectedLevel < 8)
                    selectedLevel++;
                Debug.Log("Selected Level: " + selectedLevel);
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
}