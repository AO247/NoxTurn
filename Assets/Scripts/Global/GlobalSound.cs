using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSound : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event turnEvent;

    public void PlayTurn()
    {
        turnEvent.Post(gameObject);
        Debug.Log("Footstep from player");
    }
}
