using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;


public class Floor : MonoBehaviour
{
    [SerializeField] private GameObject cameraPos;
    [SerializeField] private GameObject player;

    public float turnSpeed = 50f;
    public InputActionReference worldLeft;
    // Docelowa rotacja
    private Quaternion targetRotation;
    private Quaternion baseRotation;
    private bool isRotating = false;
    //private bool isPressed = false;
    private float time = 0.0f;

   // AudioManager audioManager;
    //private void Awake()
    //{
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    //}
    private void Start()
    {
        // Ustawienie pocz�tkowej rotacji jako docelowej, aby nie wykonywa� rotacji od razu
        targetRotation = transform.rotation;
        baseRotation = transform.rotation;
        cameraPos = GameObject.FindGameObjectWithTag("CameraPos");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {

        if (!isRotating)
        {
            time += Time.deltaTime;
            if (time > 1.0f)
            {
                targetRotation = baseRotation;
                isRotating = true;
                //isPressed = false;
            }
            if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyUp(KeyCode.JoystickButton4)) // Fire1 for LB
            {
                targetRotation = baseRotation;
                targetRotation *= Quaternion.Euler(0, -90f, 0);
                baseRotation = targetRotation;
                isRotating = true;
                //isPressed = false;
            }
            else if (Input.GetKeyUp(KeyCode.E) || Input.GetKeyUp(KeyCode.JoystickButton5)) // Fire2 for RB
            {
                targetRotation = baseRotation;
                targetRotation *= Quaternion.Euler(0, 90f, 0);
                baseRotation = targetRotation;
                isRotating = true;
                //isPressed = false;
            }
        }
        

        // P�ynne obracanie w stron� docelowej rotacji
        if (isRotating)
        {
            
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            cameraPos.transform.position = player.transform.position;
            // Sprawdzanie, czy osi�gni�to docelow� rotacj�
            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation; // Ustawienie dok�adnej warto�ci ko�cowej
                isRotating = false; // Zako�czenie obrotu
            }
        }
    }
}
