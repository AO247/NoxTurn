using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 2f; // Prêdkoœæ obrotu
    [SerializeField] private float _smoothTime = 0.2f; // Czas wyg³adzania ruchu
    private int _index = 0;
    private bool _start = true;
    private bool _end = false;
    private Vector3 _currentVelocity; // Potrzebne do SmoothDamp
    private Transform _transform; //zcacheowany transform

    private void Awake()
    {
        _transform = transform;
    }

    void Start()
    {
        if (_waypoints.Count == 0)
            Debug.LogError("Brak way pointów dla obiektu: " + gameObject.name);
        else
        {
            _transform.position = _waypoints[0].position;
            if (_waypoints.Count > 1)
                _index = 1; // Ustaw indeks na nastêpny waypoint po starcie
        }

    }


    void Update()
    {
        if (_waypoints.Count <= 1)
        {
            return;
        }

        Vector3 _destination = _waypoints[_index].position;


        // Obliczamy now¹ pozycjê
        Vector3 newPos = Vector3.SmoothDamp(_transform.position, _destination, ref _currentVelocity, _smoothTime, _speed);
        _transform.position = newPos;

        // Obliczamy kierunek poruszania siê
        Vector3 moveDirection = _destination - _transform.position;
        if (moveDirection != Vector3.zero) // Unikamy dzielenia przez zero
        {
            // Obliczamy rotacje
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            // Obracamy obiekt w kierunku ruchu
            _transform.rotation = Quaternion.Slerp(_transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }



        // Sprawdzamy, czy osi¹gnêliœmy waypoint
        float distance = Vector3.Distance(_transform.position, _destination);
        if (distance <= 0.5f)
        {
            UpdateIndex();
        }

    }

    private void UpdateIndex()
    {
        if (_start)
        {
            _index++;
        }
        else
        {
            _index--;
        }
        if (_index == 0)
        {
            _start = true;
        }
        else if (_index >= _waypoints.Count - 1)
        {
            _start = false;
        }
    }
}