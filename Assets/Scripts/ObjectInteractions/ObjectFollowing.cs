using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectFollowing : MonoBehaviour
{
    [SerializeField] private List<GameObject> _waypoints;
    [SerializeField] private float _speed = 5;
    private int _index = 0;
    private bool _start = true;
    private bool _end = false;
    private bool _pressed = false;
    private Vector3 _destination;

    void Start()
    {
    }

    public void Pressed()
    {
        if (!_pressed) { _pressed = true; }
    }

    void Update()
    {

        if (_pressed)
        {
            Vector3 _destination = _waypoints[_index].transform.position;
            Vector3 newPos = Vector3.MoveTowards(transform.position, _waypoints[_index].transform.position, _speed * Time.deltaTime);
            transform.position = newPos;
            float distance = Vector3.Distance(transform.position, _destination);

            if (_start)
            {
                if (distance <= 0.05)
                {
                    _index++;
                }
            }
            else
            {
                if (distance <= 0.05)
                {
                    _index--;
                }
            }

            if (_index == 0) 
            {
                _start = true;  
            }
            else if(_index == 1 && _end && _start)
            {
                _pressed = false;
                _end = false;
            }
            else if (_index == _waypoints.Count - 1) 
            { 
                _start = false;
                _end = true;
            }

        }
        


    }
}
