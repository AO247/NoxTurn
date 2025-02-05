using UnityEngine;

public class ItemSound : MonoBehaviour
{
    [SerializeField]
    private AK.Wwise.Event itemEvent;

    public void PlayItem()
    {
        itemEvent.Post(gameObject);
    }
}
