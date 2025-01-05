using UnityEngine;

public class FADINGCANVAS : MonoBehaviour
{
    private CanvasGroup _can; // CanvasGroup komponent
    private bool _start = false; // Czy rozpocz¹æ fade-out
    private bool _end = false;   // Czy rozpocz¹æ fade-in

    void Start()
    {
        _can = GetComponent<CanvasGroup>();
        _can.alpha = 1.0f; // Pocz¹tkowa wartoœæ alpha (pe³na widocznoœæ)
        _start = true;     // Rozpocznij fade-out po starcie
    }

    // Wywo³aj fade-in
    public void StartFadeIn()
    {
        _end = true;
        _start = false;
    }

    // Wywo³aj fade-out
    public void StartFadeOut()
    {
        _start = true;
        _end = false;
    }

    void Update()
    {
        // Fade-out (zmniejszanie alpha)
        if (_start)
        {
            _can.alpha -= Time.deltaTime / 2.0f; // Powolne zmniejszanie alpha
            if (_can.alpha <= 0)
            {
                _can.alpha = 0;
                _start = false; // Zatrzymaj fade-out
            }
        }
        // Fade-in (zwiêkszanie alpha)
        else if (_end)
        {
            _can.alpha += Time.deltaTime / 2.0f; // Powolne zwiêkszanie alpha
            if (_can.alpha >= 1)
            {
                _can.alpha = 1;
                _end = false; // Zatrzymaj fade-in
            }
        }
    }
}
