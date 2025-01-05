using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    public InputAction rotLeft; // Akcja obrotu w lewo
    public InputAction rotRight; // Akcja obrotu w prawo
    public PlayerInput playerInput;
    GameObject player; // Obiekt gracza
    public float turnSpeed = 80f; // Prêdkoœæ obrotu

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Aktywacja akcji
        rotLeft.Enable();
        rotRight.Enable();
    }

    private void OnDestroy()
    {
        // Wy³¹czanie akcji
        rotLeft.Disable();
        rotRight.Disable();
    }

    private void Update()
    {
        transform.position = player.transform.position; // Ustawienie kamery na pozycji gracza
        // Odczyt wartoœci akcji
        bool isRotLeftPressed = rotLeft.ReadValue<float>() > 0;
        bool isRotRightPressed = rotRight.ReadValue<float>() > 0;

        // Obracanie kamery
        if (isRotLeftPressed)
        {
            transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }
        if (isRotRightPressed)
        {
            transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }
    }
}
