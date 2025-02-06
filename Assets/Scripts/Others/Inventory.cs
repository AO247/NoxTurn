using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class Inventory : MonoBehaviour
{
    public Color selectedColor = new Color();
    public Color defaultColor = new Color();
    public int selectedSlot = 0;
    public GameObject itemDescription;

    public InputActionReference inventoryOpen;
    public InputActionReference up;
    public InputActionReference down;
    public InputActionReference information;

    public bool[] isFull;
    public GameObject[] slots;

    private Vector3 originalPosition;
    private Vector3 leftPosition;
    private Vector3 targetPosition; // Celowa pozycja
    private bool isClosed = true;
    public GameObject inventory;

    public bool notification = false;
    public bool moving = false;

    private bool itemDescriptionActive = false;

    public static bool needUpadate = false;

    [SerializeField] private float moveSpeed = 5f; // Prêdkoœæ ruchu

    void Start()
    {
        originalPosition = new Vector3(0.036f * Screen.width, Screen.height/2, 0.0f);
        leftPosition = new Vector3(originalPosition.x - (0.0677f * Screen.width), originalPosition.y, 0.0f);
        targetPosition = leftPosition; // Startowa pozycja
        inventory.transform.position = leftPosition;
        slotHighlight();
    }

    void updatePosition()
    {
        originalPosition = new Vector3(0.036f * Screen.width, Screen.height / 2, 0.0f);
        leftPosition = new Vector3(originalPosition.x - (0.0677f * Screen.width), originalPosition.y, 0.0f);
        targetPosition = leftPosition; // Startowa pozycja
        inventory.transform.position = leftPosition;
        isClosed = true;
        notification = false;
        moving = false;
        itemDescriptionVisibility(false);

        needUpadate = false;
    }

    void Update()
    {
        if (needUpadate)
        {
            updatePosition();
        }
        if (!PauseMenu.isPaused)
        {
            if (!isClosed)
            {
                if (up.action.triggered)
                {
                    selectedSlot--;

                    if (selectedSlot < 0)
                    {
                        selectedSlot = slots.Length - 1;
                    }
                    else if (selectedSlot > slots.Length - 1)
                    {
                        selectedSlot = 0;
                    }
                    slotHighlight();
                    GetItemDescription();
                }
                if (down.action.triggered)
                {
                    selectedSlot++;

                    if (selectedSlot < 0)
                    {
                        selectedSlot = slots.Length - 1;
                    }
                    else if (selectedSlot > slots.Length - 1)
                    {
                        selectedSlot = 0;
                    }
                    slotHighlight();
                    GetItemDescription();
                }
                if (information.action.triggered)
                {
                    itemDescriptionVisibility();
                }

                if (information.action.triggered)
                {
                    Debug.Log("Information about " + slots[selectedSlot].name);
                }

            }
            // Prze³¹czanie celu po naciœniêciu I
            if (inventoryOpen.action.triggered)
            {
                notification = false;
                moving = false;
                if (isClosed)
                    targetPosition = originalPosition;
                else
                    targetPosition = leftPosition;
                isClosed = !isClosed;
                itemDescriptionVisibility(false);
            }

            if (notification && isClosed)
            {
                if (!moving)
                {
                    targetPosition = originalPosition;
                    moving = true;
                }
                if (originalPosition.x - inventory.transform.position.x < 5)
                {
                    targetPosition = leftPosition;
                    notification = false;
                    moving = false;
                }
            }

            // P³ynne przemieszczanie do celu
            inventory.transform.position = Vector3.Lerp(
            inventory.transform.position, targetPosition, Time.deltaTime * moveSpeed);
        }
    }
    private void slotHighlight()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i == selectedSlot)
            {
                Debug.Log("Selected slot: " + i);
                slots[i].GetComponent<Image>().color = selectedColor;
            }
            else
            {
                slots[i].GetComponent<Image>().color = defaultColor;
            }
        }
    }


    public bool findKey()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // Sprawdzamy, czy w danym slocie jest jakiœ obiekt (przedmiot zosta³ dodany jako dziecko)
            if (slots[i].transform.childCount > 0)
            {
                // Pobieramy pierwsze dziecko – zak³adamy, ¿e w slocie jest tylko jeden item
                GameObject itemInSlot = slots[i].transform.GetChild(0).gameObject;

                // Sprawdzamy, czy nazwa obiektu (lub inna w³aœciwoœæ, np. tag) odpowiada "Key"
                // UWAGA: Zwróæ uwagê, ¿e nazwa GameObjectu mo¿e byæ inna ni¿ nazwa komponentu!
                if (itemInSlot.GetComponent<ItemDescription>().name == "Key")
                {
                    // Usuwamy przedmiot (klucz) z ekwipunku
                    Destroy(itemInSlot);
                    // Zak³adamy, ¿e masz dostêp do tablicy isFull (np. poprzez referencjê do inventory)
                    isFull[i] = false;
                    return true;
                }
            }
        }
        return false;
    }

    private void itemDescriptionVisibility(bool status = true)
    {
        if(status)
        {
            itemDescriptionActive = !itemDescriptionActive;
        }
        else {
            itemDescriptionActive = false;
        }
        itemDescription.SetActive(itemDescriptionActive);
    }
    public void GetItemDescription()
    {
        itemDescription.transform.position = slots[selectedSlot].transform.position;
        itemDescription.GetComponent<TextMeshProUGUI>().SetText(slots[selectedSlot].GetComponent<ItemDescription>().itemDescription);
    }
}
