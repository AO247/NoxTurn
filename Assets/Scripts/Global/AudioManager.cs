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
        if (!SFXSource.isPlaying) // Jeœli dŸwiêk jest ju¿ odtwarzany, zatrzymaj go
        {
            SFXSource.clip = clip; // Ustawienie klipu
            SFXSource.loop = true; // W³¹czenie pêtli
            SFXSource.Play(); // Odtwarzanie
        }

    }
    public void StopPlaying()
    {
        SFXSource.Stop(); // Zatrzymanie dŸwiêku
        SFXSource.loop = false; // Wy³¹czenie pêtli na wypadek ponownego odtworzenia
    }

}
