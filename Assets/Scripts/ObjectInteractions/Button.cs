using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Button : MonoBehaviour
{
    [SerializeField] ObjectFollowing _object;
    [SerializeField] Parkour _parkour;
    [SerializeField] GameObject _text;
    public InputActionReference interaction;

    private Renderer _renderer;
    private bool _enter = false;
    private bool _pressed = false;
    private float _timer = 1;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {   
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (_enter)
        {
            if(interaction.action.triggered)
            {
                _pressed = true;
                if (_object != null)
                {
                    _object.Pressed();
                }
                if (_parkour != null)
                {
                    _parkour.Pressed();
                }
            }
        }
        if (_pressed)
        {
            if (_timer > 0)
            {
                _renderer.material.color = Color.red;
                _timer -= Time.deltaTime;
            } else
            {   
                _renderer.material.color = Color.blue;
                _pressed = false;
                _timer = 1;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        { 
            _enter = true;
            _text.GetComponent<Renderer>().enabled = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {   
            _text.GetComponent<Renderer>().enabled = false;
            _enter = false;
        }
    }
}
