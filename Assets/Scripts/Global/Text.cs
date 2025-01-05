using UnityEngine;

public class Text : MonoBehaviour
{
    private Transform _trans;
    private Vector3 _offset = new Vector3(0, 180, 0);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _trans = GameObject.Find("Camera").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(_trans);
        transform.Rotate(_offset);
    }
}
