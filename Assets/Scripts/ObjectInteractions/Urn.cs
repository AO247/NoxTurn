using UnityEngine;

public class Urn : MonoBehaviour
{
    bool isMoving = false;
    Vector3 previousPosition;

    void Start()
    {
        // Zapamiêtujemy pocz¹tkow¹ pozycjê
        previousPosition = transform.position;
    }

    void Update()
    {
        // Sprawdzamy, czy pozycja zmieni³a siê w porównaniu z poprzedni¹ klatk¹
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

        // Aktualizujemy poprzedni¹ pozycjê do obecnej pozycji na koñcu Update()
        previousPosition = transform.position;
    }
}
