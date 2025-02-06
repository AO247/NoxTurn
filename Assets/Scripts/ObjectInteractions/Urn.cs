using UnityEngine;

public class Urn : MonoBehaviour
{
    [SerializeField] private UrnSound urnSound;
    bool isMoving = false;
    Vector3 previousPosition;
    float time = 2.0f;

    void Start()
    {
        // Zapamiêtujemy pocz¹tkow¹ pozycjê
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
            // Sprawdzamy, czy pozycja zmieni³a siê w porównaniu z poprzedni¹ klatk¹
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

            // Aktualizujemy poprzedni¹ pozycjê do obecnej pozycji na koñcu Update()
            previousPosition = transform.position;
        }
    }
}
