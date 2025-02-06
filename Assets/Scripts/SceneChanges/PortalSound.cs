using UnityEngine;

public class PortalSound : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event portalEvent;

    public void PlayPortal()
    {
        portalEvent.Post(gameObject);
    }
}
