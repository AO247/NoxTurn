using TMPro;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private ItemSound itemSound;
    private Inventory inventory;
    public GameObject item;
    private bool isPickedUp = false; // Flaga zapobiegaj¹ca wielokrotnemu podniesieniu

    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPickedUp) return;
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
                    if(!isPickedUp)
                    {
                        itemSound.PlayItem();
                        isPickedUp = true; // Oznacz przedmiot jako podniesiony
                        inventory.isFull[i] = true;
                        Instantiate(item, inventory.slots[i].transform, false);
                        inventory.slots[i].GetComponent<ItemDescription>().itemDescription = item.GetComponent<ItemDescription>().itemDescription;
                        inventory.slots[i].GetComponent<ItemDescription>().name = item.GetComponent<ItemDescription>().name;
                        inventory.notification = true;
                        inventory.GetItemDescription();
                        Destroy(gameObject); // Usuñ przedmiot z poziomu
                    }

                    break;
                }
            }
        }
    }
}
