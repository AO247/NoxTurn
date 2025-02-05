using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public Vector3 _originalPos;
    bool _isPressed = false;
    private float _maxDownPos = 0.1f;

    [SerializeField] private Transform _target;
    public Vector3 _targetOriginalPos;
    [SerializeField] private float _targetMaxTransPos;
    [SerializeField] private Vector3 _targetMovePosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _originalPos = transform.position;
        _targetOriginalPos = _target.position;
    }
    
    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Movable")){
            _isPressed = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Movable")){
            _isPressed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_isPressed){
            if(Vector3.Distance(_target.position, _targetOriginalPos) < _targetMaxTransPos){
                _target.Translate(_targetMovePosition.x, _targetMovePosition.y, _targetMovePosition.z);
            }
            if(transform.position.y > _originalPos.y - _maxDownPos){
                transform.Translate(0.00f, -0.01f, 0.00f);
            }
        }
        else if(!_isPressed){
            if(Vector3.Distance(_target.position, _targetOriginalPos) > 0.2){
                _target.Translate(-_targetMovePosition.x, -_targetMovePosition.y, -_targetMovePosition.z);
            }
            if(transform.position.y < _originalPos.y){
                transform.Translate(0.00f, 0.01f, 0.00f);
            }
        }
    }
}
