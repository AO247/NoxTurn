using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{
    public InputAction rotLeft; // Akcja obrotu w lewo
    public InputAction rotRight; // Akcja obrotu w prawo
    public PlayerInput playerInput;
    public Transform player; // Obiekt gracza - zamieniono GameObject na Transform
    public float turnSpeed = 80f; // Prêdkoœæ obrotu
    public float smoothTime = 0.2f; // Czas wyg³adzania ruchu kamery
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
        if (player == null) return;
        // P³ynne pod¹¿anie kamery za graczem z offsetem
        Vector3 targetPosition = player.transform.position + offset;
        _transform.position = Vector3.SmoothDamp(_transform.position, targetPosition, ref _currentVelocity, smoothTime);

        // Odczyt wartoœci akcji
        bool isRotLeftPressed = rotLeft.ReadValue<float>() > 0;
        bool isRotRightPressed = rotRight.ReadValue<float>() > 0;

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