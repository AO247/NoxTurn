using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrnSound : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event urnEvent;

    public void PlayUrn()
    {
        urnEvent.Post(gameObject);
    }

    public void StopUrn()
    {
        urnEvent.Stop(gameObject);
    }
}
