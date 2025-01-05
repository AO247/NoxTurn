using Unity.VisualScripting;
using UnityEngine;



public class AudioManager : MonoBehaviour
{
    [Header("---------Audio Sources---------")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("---------Audio Clip---------")]
    public AudioClip background;
    public AudioClip turn;
    public AudioClip death;
    public AudioClip scorching;
    public AudioClip footsteps;

    private void Start()
    {
        //musicSource.clip = background;
        //musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
    public void PlaySFXInLoop(AudioClip clip)
    {
        if (!SFXSource.isPlaying) // Je�li d�wi�k jest ju� odtwarzany, zatrzymaj go
        {
            SFXSource.clip = clip; // Ustawienie klipu
            SFXSource.loop = true; // W��czenie p�tli
            SFXSource.Play(); // Odtwarzanie
        }

    }
    public void StopPlaying()
    {
        SFXSource.Stop(); // Zatrzymanie d�wi�ku
        SFXSource.loop = false; // Wy��czenie p�tli na wypadek ponownego odtworzenia
    }

}
