using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSound : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event gateOpenEvent;
    [SerializeField]
    private AK.Wwise.Event gateLockedEvent;

    public void PlayOpen()
    {
        gateOpenEvent.Post(gameObject);
    }

    public void PlayLock()
    {
        gateLockedEvent.Post(gameObject);
    }
}
