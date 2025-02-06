using UnityEngine;

public class Urn : MonoBehaviour
{
    [SerializeField] private UrnSound urnSound;
    bool isMoving = false;
    Vector3 previousPosition;
    float time = 2.0f;

    void Start()
    {
        // Zapami�tujemy pocz�tkow� pozycj�
        previousPosition = transform.position;
    }

    void Update()
    {
        if (time > 0.0f)
        {
            time -= Time.deltaTime;
        }
        else
        {
            // Sprawdzamy, czy pozycja zmieni�a si� w por�wnaniu z poprzedni� klatk�
            if (!previousPosition.Equals(transform.position) && !isMoving)
            {
                isMoving = true;
                urnSound.PlayUrn();
            }
            else if (previousPosition.Equals(transform.position) && isMoving)
            {
                urnSound.StopUrn();
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
}
