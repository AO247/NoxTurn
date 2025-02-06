using UnityEngine;

public class Urn : MonoBehaviour
{
    bool isMoving = false;
    Vector3 previousPosition;

    void Start()
    {
        // Zapami�tujemy pocz�tkow� pozycj�
        previousPosition = transform.position;
    }

    void Update()
    {
        // Sprawdzamy, czy pozycja zmieni�a si� w por�wnaniu z poprzedni� klatk�
        if (!previousPosition.Equals(transform.position) && !isMoving)
        {
            isMoving = true;
        }
        else if (previousPosition.Equals(transform.position) && isMoving)
        {
            isMoving = false;
        }

        if (isMoving)
        {
            Debug.Log("Moving: " + isMoving);
        }

        // Aktualizujemy poprzedni� pozycj� do obecnej pozycji na ko�cu Update()
        previousPosition = transform.position;
    }
}
