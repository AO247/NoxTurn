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



    [SerializeField] private float moveSpeed = 5f; // Prêdkoœæ ruchu

    void Start()
    {
        originalPosition = inventory.transform.position;
        leftPosition = new Vector3(originalPosition.x - 130, originalPosition.y, originalPosition.z);
        targetPosition = leftPosition; // Startowa pozycja
        inventory.transform.position = leftPosition;
        slotHighlight();
    }

    void Update()
    {
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
    public void GetAllItems()
    {
        Debug.Log("Items in inventory:");
        for (int i = 0; i < slots.Length; i++)
        {
            if (isFull[i] == true)
            {
                 Debug.Log(slots[i].name);
            }
        }
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
    private void GetItemDescription()
    {
        itemDescription.transform.position = slots[selectedSlot].transform.position;
        itemDescription.GetComponent<TextMeshProUGUI>().SetText(slots[selectedSlot].GetComponent<ItemDescription>().itemDescription);
    }
}
