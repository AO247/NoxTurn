using System;
using UnityEngine;
using UnityEngine.Analytics;

public class KeyInteraction : MonoBehaviour
{
    public Inventory inventory;
    public Transform leftGate;
    public Transform rightGate;

    [SerializeField] GameObject text;
    private bool enter = false;
    private bool opened = false;
    private Quaternion targetRotationRightGate = Quaternion.Euler(0, -75f, 0);
    private Quaternion targetRotationLeftGate = Quaternion.Euler(0, -75f, 0);
    public float turnSpeed = 50f;
    private bool isRotating = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(enter && Input.GetKeyDown(KeyCode.F))
        {
            if (inventory.findKey())
            {
                opened = true;
                isRotating = true;
                text.GetComponent<Renderer>().enabled = false;
            }
        }
        if (opened && isRotating)
        {
            rightGate.transform.rotation = Quaternion.RotateTowards(rightGate.transform.rotation, targetRotationRightGate, turnSpeed * Time.deltaTime);
            leftGate.transform.rotation = Quaternion.RotateTowards(leftGate.transform.rotation, targetRotationLeftGate, turnSpeed * Time.deltaTime);
            // Sprawdzanie, czy osi¹gniêto docelow¹ rotacjê
            if (Quaternion.Angle(rightGate.transform.rotation, targetRotationRightGate) < 0.1f)
            {
                rightGate.transform.rotation = targetRotationRightGate; // Ustawienie dok³adnej wartoœci koñcowej
                leftGate.transform.rotation = targetRotationLeftGate; // Ustawienie dok³adnej wartoœci koñcowej
                isRotating = false; // Zakoñczenie obrotu
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
