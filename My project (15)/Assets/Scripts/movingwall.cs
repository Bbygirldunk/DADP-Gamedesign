using UnityEngine;

public class MovingWall : MonoBehaviour
{
    public float speed = 2.0f; // Speed of the wall movement
    public float distance = 5.0f; // Distance the wall moves up and down
    public float damage = 10f; // Damage dealt to the player on collision
    public float lifetime = 20f; // Lifetime of the wall in seconds

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
        Destroy(gameObject, lifetime); // Destroy the wall after its lifetime expires
    }

    void Update()
    {
        float movement = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPosition + new Vector3(0, movement, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the wall collided with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Assuming the player has a component called "PlayerHealth"
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Deal damage to the player
            }
        }
    }
}
