using UnityEngine;

public class AudioManagerWwise : MonoBehaviour
{

    public static AudioManagerWwise AudioManagerInstance;
    [SerializeField] private AK.Wwise.Event music;

    private bool isPlaying = false;

    private void Awake()
    {
        AudioManagerInstance = this;
    }

    private void Start()
    {
        if(music.IsValid())
        {
            music.Post(gameObject);
            isPlaying = true;
        }
    }
}
