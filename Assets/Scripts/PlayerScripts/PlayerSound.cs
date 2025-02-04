using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    [SerializeField] 
    private AK.Wwise.Event footstepsEvent;
    [SerializeField]
    private AK.Wwise.Event dyingSound;
    [SerializeField]
    private AK.Wwise.Event deathSound;

    public void PlayFootstepSound()
    {
        footstepsEvent.Post(gameObject);
        Debug.Log("Footstep from player");
    }

    public void PlayDying()
    {
        dyingSound.Post(gameObject);
        Debug.Log("DyingSound");
    }

    public void PlayDeath()
    {
        deathSound.Post(gameObject);
        Debug.Log("DeathSound");
    }
}
