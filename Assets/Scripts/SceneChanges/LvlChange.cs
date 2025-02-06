using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LvlChange : MonoBehaviour
{
    private bool isPlayerInRange = false; // Czy gracz jest w zasiêgu
    private FADINGCANVAS fadingCanvas;    // Referencja do skryptu FADINGCANVAS
    player player;
    void Start()
    {
        // ZnajdŸ obiekt ze skryptem FADINGCANVAS w scenie
        fadingCanvas = Object.FindFirstObjectByType<FADINGCANVAS>();
        player = Object.FindFirstObjectByType<player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ChangeScene());
        player.canMove = false;
    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(ChangeScene());
        }
        else if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(ChangeSceneDown());
        }
    }

    private IEnumerator ChangeScene()
    {
        if (fadingCanvas != null)
        {
            fadingCanvas.StartFadeIn(); // Rozpocznij fade-in
            yield return new WaitForSeconds(2); // Poczekaj na zakoñczenie fade-in
        }
        SaveData saveData = new SaveData(); // Utwórz nowy obiekt SaveData
        if (SceneManager.GetActiveScene().buildIndex + 1 < 13)
        {
            saveData.lvlNumber = SceneManager.GetActiveScene().buildIndex + 1;
            SaveManager.SaveGameState(saveData);

            // Za³aduj nastêpn¹ scenê

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);

        }

    }
    private IEnumerator ChangeSceneDown()
    {
        if (fadingCanvas != null)
        {
            fadingCanvas.StartFadeIn(); // Rozpocznij fade-in
            yield return new WaitForSeconds(2); // Poczekaj na zakoñczenie fade-in
        }
        SaveData saveData = new SaveData(); // Utwórz nowy obiekt SaveData
        if (SceneManager.GetActiveScene().buildIndex - 1 > -1)
        {
            saveData.lvlNumber = SceneManager.GetActiveScene().buildIndex - 1;
            SaveManager.SaveGameState(saveData);

            // Za³aduj nastêpn¹ scenê

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        else
        {
            SceneManager.LoadScene(0);

        }

    }
}
