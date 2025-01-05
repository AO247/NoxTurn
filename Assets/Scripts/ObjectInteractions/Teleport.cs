using Unity.VisualScripting;
using UnityEngine;


public class Teleport : MonoBehaviour
{

    [SerializeField] private Collider _direction;
    private Collider _player;
    private float _timer = 2;
    private bool _entered = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_entered)
        {
            _timer -= Time.deltaTime;
        }
        if(_timer < 0)
        {
            if (_player != null)
            {
                _player.transform.position = _direction.transform.position;

            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _entered = true;
            _player = other;      
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _entered = false;
            _timer = 2;
        }
    }
}
