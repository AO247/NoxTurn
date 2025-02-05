using System;
using UnityEngine;
using UnityEngine.Analytics;

public class KeyInteraction : MonoBehaviour
{
    public Inventory inventory;
    [SerializeField] GameObject text;
    private bool enter = false;
    private bool opened = false;
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
                text.GetComponent<Renderer>().enabled = false;
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
