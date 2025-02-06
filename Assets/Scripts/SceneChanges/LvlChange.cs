using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LvlChange : MonoBehaviour
{
    private bool isPlayerInRange = false; // Czy gracz jest w zasi�gu
    private FADINGCANVAS fadingCanvas;    // Referencja do skryptu FADINGCANVAS

    void Start()
    {
        // Znajd� obiekt ze skryptem FADINGCANVAS w scenie
        fadingCanvas = Object.FindFirstObjectByType<FADINGCANVAS>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ChangeScene());
    }


    private void Update()
    {

    }

    private IEnumerator ChangeScene()
    {
        if (fadingCanvas != null)
        {
            fadingCanvas.StartFadeIn(); // Rozpocznij fade-in
            yield return new WaitForSeconds(2); // Poczekaj na zako�czenie fade-in
        }
        SaveData saveData = new SaveData(); // Utw�rz nowy obiekt SaveData
        saveData.lvlNumber = SceneManager.GetActiveScene().buildIndex + 1;
        SaveManager.SaveGameState(saveData);

        // Za�aduj nast�pn� scen�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    }
}
