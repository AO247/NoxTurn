using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Experimental.GlobalIllumination;
using static UnityEngine.UI.Image;

public class Enemy : MonoBehaviour
{
    [SerializeField] private List<Transform> _waypoints;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _rotationSpeed = 2f; // Pr�dko�� obrotu
    [SerializeField] private float _smoothTime = 0.2f; // Czas wyg�adzania ruchu
    private int _index = 0;
    private bool _start = true;
    private bool _end = false;
    private Vector3 _currentVelocity; // Potrzebne do SmoothDamp
    private Transform _transform; //zcacheowany transform
    private Transform playerTransform;
    private NavMeshAgent nav;
    public  Light light;
    public bool playerDetected = false;

    private void Awake()
    {
        _transform = transform;
    }

    void Start()
    {
        if (_waypoints.Count == 0)
            Debug.LogError("Brak way point�w dla obiektu: " + gameObject.name);
        else
        {
            _transform.position = _waypoints[0].position;
            if (_waypoints.Count > 1)
                _index = 1; // Ustaw indeks na nast�pny waypoint po starcie
        }
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (Vector3.Distance(_transform.position, playerTransform.position) < 2.0f)
        {
            playerTransform.gameObject.GetComponent<player>().HandleDeath();
        }
        PlayerDetection();
        if (!playerDetected)
        {
            nav.destination = playerTransform.position;

            if (_waypoints.Count <= 1)
            {
                return;
            }

            nav.destination = _waypoints[_index].position;



            // Sprawdzamy, czy osi�gn�li�my waypoint
            float distance = Vector3.Distance(_transform.position, _waypoints[_index].position);
            if (distance <= 2.0f)
            {
                UpdateIndex();
            }
        }
        else 
        {
            float distance = Vector3.Distance(_transform.position, nav.destination);
            if (distance <= 2.0f)
            {
                playerDetected = false;
            }
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
    private void PlayerDetection()
    {
        Vector3 lightDirection = transform.position - playerTransform.position; //Kierunek �wiat�a od obiektu do punktu
        float distanceToLight = lightDirection.magnitude; // D�ugo�� wektora = odleg�o��

        if (distanceToLight <= light.range) // Sprawdzamy czy obiekt jest w zasiegu
        {
            float angle = Vector3.Angle(-light.transform.forward, lightDirection); // Obliczamy k�t pomi�dzy kierunkiem �wiat�a a pozycj� obiektu
            if (angle <= light.spotAngle / 2) // Sprawdzamy czy obiekt jest w sto�ku �wiat�a
            {
                if (!Physics.Raycast(playerTransform.position, (light.transform.position - playerTransform.position), out RaycastHit hit, distanceToLight))
                {
                    Debug.DrawRay(playerTransform.position, (light.transform.position - playerTransform.position), Color.magenta);
                    nav.destination = playerTransform.position;
                    playerDetected = true;

                }
                else
                {
                    Debug.DrawRay(playerTransform.position, (light.transform.position - playerTransform.position), Color.black);

                }
            }
            else
            {
                Debug.DrawRay(light.transform.position, (playerTransform.position - light.transform.position), Color.white); // Obiekt poza sto�kiem, kolor cyan
            }
        }
        else
        {
            Debug.DrawRay(light.transform.position, (playerTransform.position - light.transform.position), Color.grey); // Obiekt poza zasi�giem, kolor zolty
        }
    }
}