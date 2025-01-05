using TMPro;
using UnityEngine;


public class TurningBlob : MonoBehaviour
{
    //[SerializeField] private Transform _cap;
    private Rigidbody _rb;
    private bool _blob = false;
    private Renderer _renderer;
    private player _player;
    public Transform trans;
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
            if (!_blob) 
            { 
            upperY = trans.position.y;
            lowerY = upperY - 300.0f;
            }
            _blob = true;
            _player._isImmortal = true;
        }
        hair.SetActive(_blob);
        if (_blob)
        {
            //_renderer.enabled = false;
            //_cap.localRotation = Quaternion.Euler(10f, 0, 0);
            //_cap.localPosition = new Vector3(0, -0.596f, -0.054f);
            trans.position = new Vector3(trans.position.x, lowerY, trans.position.z);
            if (!_rb.linearVelocity.Compare(Vector3.zero,1))
            {
                _blob = false;
                _player._isImmortal = false;
                trans.position = new Vector3(trans.position.x, upperY, trans.position.z);
                //_cap.localRotation = Quaternion.Euler(0, 0, 0);
                //_cap.localPosition = new Vector3(0, 0.191f, 0.457f);
                //_renderer.enabled = true;
            }
            
        }
       // Debug.Log(_player._isImmortal);

    }
}
