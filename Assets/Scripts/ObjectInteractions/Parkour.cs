using UnityEngine;

public class Parkour : MonoBehaviour
{
    private float _time = 0;
    private Vector3 _orgPos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _orgPos = transform.position;
    }
    public void Pressed()
    {
        _time = 5;
    }
    // Update is called once per frame
    void Update()
    {
        if (_time > 0)
        {
            transform.position = _orgPos;
            _time -= Time.deltaTime;

        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, 59.0f);
        }
    }
}
