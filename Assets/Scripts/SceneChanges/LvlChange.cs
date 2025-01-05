using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LvlChange : MonoBehaviour
{
    private bool isPlayerInRange = false; // Czy gracz jest w zasiêgu
    private FADINGCANVAS fadingCanvas;    // Referencja do skryptu FADINGCANVAS

    void Start()
    {
        // ZnajdŸ obiekt ze skryptem FADINGCANVAS w scenie
        fadingCanvas = Object.FindFirstObjectByType<FADINGCANVAS>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; // Gracz w zasiêgu
            Debug.Log("Gracz w zasiêgu! Wciœnij 'F' aby przejœæ do nowej sceny.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false; // Gracz opuœci³ zasiêg
        }
    }

    private void Update()
    {
        // SprawdŸ, czy gracz nacisn¹³ klawisz F i jest w zasiêgu
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
            yield return new WaitForSeconds(2); // Poczekaj na zakoñczenie fade-in
        }

        // Za³aduj nastêpn¹ scenê
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
