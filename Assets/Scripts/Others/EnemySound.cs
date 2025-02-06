using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event footstepsEvent;

    public void PlayFootstepSound()
    {
        GroundSwitch();
        footstepsEvent.Post(gameObject);
        Debug.Log("Footstep from enemy");
    }
    private void GroundSwitch()
    {
        RaycastHit hit;
        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, -Vector3.up);
        Material surfaceMaterial;

        if (Physics.Raycast(ray, out hit, 1.0f, Physics.AllLayers, QueryTriggerInteraction.Ignore))
        {
            Renderer surfaceRenderer = hit.collider.GetComponentInChildren<Renderer>();
            if (surfaceRenderer)
            {
                Debug.Log(surfaceRenderer.material.name);
                if (surfaceRenderer.material.name.Contains("Floor"))
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
