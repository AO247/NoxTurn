using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool[] isFull;
    public GameObject[] slots;

    private Vector3 originalPosition;
    private Vector3 leftPosition;
    private Vector3 targetPosition; // Celowa pozycja
    private bool isMovedLeft = true;
    public GameObject inventory;

    public bool notification = false;
    public bool moving = false;


    [SerializeField] private float moveSpeed = 5f; // Prêdkoœæ ruchu

    void Start()
    {
        originalPosition = inventory.transform.position;
        leftPosition = new Vector3(originalPosition.x - 130, originalPosition.y, originalPosition.z);
        targetPosition = leftPosition; // Startowa pozycja
        inventory.transform.position = leftPosition;
    }

    void Update()
    {
        // Prze³¹czanie celu po naciœniêciu I
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown(KeyCode.JoystickButton7))
        {
            notification = false;
            moving = false;
            if (isMovedLeft)
                targetPosition = originalPosition;
            else
                targetPosition = leftPosition;
            isMovedLeft = !isMovedLeft;
        }

        if (notification && isMovedLeft)
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
