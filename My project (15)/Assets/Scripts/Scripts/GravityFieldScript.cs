using UnityEngine;

public class GravityFieldScript : MonoBehaviour
{
    public float gravityForce = 10f;   // Strength of the pull/push force
    public Transform gravityCenter;    // Center point to pull the player towards
    public bool pullPlayer = true;     // If true, pulls player; if false, pushes player away

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();

            if (playerRigidbody != null)
            {
                // Calculate direction from player to the center of gravity
                Vector3 directionToCenter = gravityCenter.position - other.transform.position;

                if (!pullPlayer)  // If pushing instead of pulling
                {
                    directionToCenter = -directionToCenter;
                }

                // Apply force to the player in the direction of the gravity center
                playerRigidbody.AddForce(directionToCenter.normalized * gravityForce, ForceMode.Acceleration);

                // Optionally, prevent unwanted rotation
                playerRigidbody.angularVelocity = Vector3.zero;

                
            }
        }
    }
}
