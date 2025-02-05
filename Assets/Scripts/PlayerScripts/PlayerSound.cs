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
    [SerializeField]
    private AK.Wwise.Event shadowInSound;
    [SerializeField]
    private AK.Wwise.Event shadowOutSound;

    public void PlayFootstepSound()
    {
        GroundSwitch();
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

    public void PlayIn()
    {
        shadowInSound.Post(gameObject);
        Debug.Log("DeathSound");
    }

    public void PlayOut()
    {
        shadowOutSound.Post(gameObject);
        Debug.Log("DeathSound");
    }

    private void GroundSwitch()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, -Vector3.up);
        Material surfaceMaterial;

        if(Physics.Raycast(ray, out hit, 1.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            Renderer surfaceRenderer = hit.collider.GetComponentInChildren<Renderer>();
            if(surfaceRenderer)
            {
                Debug.Log(surfaceRenderer.material.name);
                if(surfaceRenderer.material.name.Contains("Floor"))
                {
                    AkSoundEngine.SetSwitch("Footsteps", "Grass", gameObject);
                }
                else
                {
                    AkSoundEngine.SetSwitch("Footsteps", "Stone", gameObject);
                }
            }
        }
    }
}
