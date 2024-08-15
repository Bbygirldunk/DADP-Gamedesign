using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public float speed = 2.0f;
    public float distance = 5.0f;
    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPosition + new Vector3(0, movement, 0);
    }
}

