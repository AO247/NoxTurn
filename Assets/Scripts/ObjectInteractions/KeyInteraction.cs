using System;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.InputSystem;

public class KeyInteraction : MonoBehaviour
{
    public Inventory inventory;
    public Transform leftGate;
    public Transform rightGate;
    public int direction = 1;
    [SerializeField] GameObject text;
    [SerializeField] private ObjectSound objectSound;
    public InputActionReference interaction;
    public bool isNormalGate = false;
    private bool enter = false;
    private bool opened = false;
    private bool door = false;
    private Quaternion targetRotationRightGate;
    private Quaternion targetRotationLeftGate;
    public float turnSpeed = 50f;
    private bool isRotating = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetRotationRightGate = Quaternion.Euler(0, direction * -75f, 0);
        if (isNormalGate)
        {
            targetRotationLeftGate = Quaternion.Euler(0, -direction * -75f, 0);
        }
        else
        {
            targetRotationLeftGate = Quaternion.Euler(0, direction * -75f, 0);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(enter && (interaction.action.triggered))
        {
            if (inventory.findKey())
            {
                opened = true;
                isRotating = true;
                door = true;
                text.GetComponent<Renderer>().enabled = false;
            }
            else
            {
                objectSound.PlayLock();
            }
        }

        if (opened && isRotating)
        {

            rightGate.transform.rotation = Quaternion.RotateTowards(rightGate.transform.rotation, targetRotationRightGate, turnSpeed * Time.deltaTime);
            leftGate.transform.rotation = Quaternion.RotateTowards(leftGate.transform.rotation, targetRotationLeftGate, turnSpeed * Time.deltaTime);
            if (door)
            {
                objectSound.PlayOpen();
                door = false;
            }
            // Sprawdzanie, czy osi�gni�to docelow� rotacj�
            if (Quaternion.Angle(rightGate.transform.rotation, targetRotationRightGate) < 0.1f)
            {
                rightGate.transform.rotation = targetRotationRightGate; // Ustawienie dok�adnej warto�ci ko�cowej
                leftGate.transform.rotation = targetRotationLeftGate; // Ustawienie dok�adnej warto�ci ko�cowej
                isRotating = false; // Zako�czenie obrotu
            }
        }
  
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !opened)
        {
            enter = true;
            text.GetComponent<Renderer>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player") && !opened)
        {
            text.GetComponent<Renderer>().enabled = false;
            enter = false;
        }
    }
}
