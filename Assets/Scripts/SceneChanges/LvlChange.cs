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
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Gracz w zasi�gu
            Debug.Log("Gracz w zasi�gu! Wci�nij 'F' aby przej�� do nowej sceny.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Gracz opu�ci� zasi�g
        }
    }

    private void Update()
    {
        // Sprawd�, czy gracz nacisn�� klawisz F i jest w zasi�gu
        if (isPlayerInRange && (Input.GetKeyDown(KeyCode.F) || Input.GetButtonDown("Fire3")))
        {
            StartCoroutine(ChangeScene());
        }
    }

    private IEnumerator ChangeScene()
    {
        if (fadingCanvas != null)
        {
            fadingCanvas.StartFadeIn(); // Rozpocznij fade-in
            yield return new WaitForSeconds(2); // Poczekaj na zako�czenie fade-in
        }

        // Za�aduj nast�pn� scen�
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
