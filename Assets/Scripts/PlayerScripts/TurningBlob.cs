using TMPro;
using UnityEngine;


public class TurningBlob : MonoBehaviour
{
    //[SerializeField] private Transform _cap;
    private Rigidbody _rb;
    public bool _blob = false;
    private Renderer _renderer;
    private player _player;
    public GameObject hair;
    float upperY, lowerY; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        _player = GetComponent<player>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.JoystickButton4)
            || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.JoystickButton5)) // Fire1 for LB
        {
            _rb.linearVelocity = Vector3.zero;
            _blob = true;
            _player._isImmortal = true;
        }
        hair.SetActive(_blob);
        if (_blob)
        {
            _player.PlayerVisibility(false);
            if (!_rb.linearVelocity.Compare(Vector3.zero,1))
            {
                _player.PlayerVisibility(true);
                _blob = false;
                _player._isImmortal = false;

            }
            
        }

    }
}
