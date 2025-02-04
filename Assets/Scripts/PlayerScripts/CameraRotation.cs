using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    public InputActionReference rotLeft; // Akcja obrotu w lewo
    public InputActionReference rotRight; // Akcja obrotu w prawo
    public PlayerInput playerInput;
    public Transform player; // Obiekt gracza - zamieniono GameObject na Transform
    public float turnSpeed = 80f; // Pr�dko�� obrotu
    public float smoothTime = 0.2f; // Czas wyg�adzania ruchu kamery
    public Vector3 offset = Vector3.zero; // Dodatkowy offset kamery

    private Vector3 _currentVelocity; // Potrzebne do SmoothDamp
    private Transform _transform; // Zcacheowany transform

    private void Awake()
    {
        _transform = transform; // Zcacheowanie transform
    }

    private void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        // Aktywacja akcji za pomoc� w�a�ciwo�ci .action
        rotLeft.action.Enable();
        rotRight.action.Enable();
    }

    private void OnDestroy()
    {
        // Wy��czanie akcji
        rotLeft.action.Disable();
        rotRight.action.Disable();
    }

    private void Update()
    {
        if (player == null) return;

        // P�ynne pod��anie kamery za graczem z offsetem
        Vector3 targetPosition = player.position + offset;
        _transform.position = Vector3.SmoothDamp(_transform.position, targetPosition, ref _currentVelocity, smoothTime);

        // Odczyt warto�ci akcji (korzystamy z w�a�ciwo�ci .action)
        bool isRotLeftPressed = rotLeft.action.ReadValue<float>() > 0;
        bool isRotRightPressed = rotRight.action.ReadValue<float>() > 0;

        // Obracanie kamery
        if (isRotLeftPressed)
        {
            _transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        }
        if (isRotRightPressed)
        {
            _transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
        }
    }
}
 