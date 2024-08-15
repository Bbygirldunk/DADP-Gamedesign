using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string itemName; // Name of the item to be collected
    public float respawnTime = 5f; // Time in seconds before the item respawns
    public Vector3 respawnAreaMin; // Minimum coordinates for the respawn area
    public Vector3 respawnAreaMax; // Maximum coordinates for the respawn area

    private bool isCollected = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!isCollected && other.CompareTag("Player"))
        {
            CollectItem();
        }
    }

    private void CollectItem()
    {
        isCollected = true;
        Debug.Log("Collected: " + itemName);

        // Add the item to the player's inventory here (if you have an inventory system)

        gameObject.SetActive(false); // Hide the object after collection
        Invoke("Respawn", respawnTime); // Schedule the respawn after the delay
    }

    private void Respawn()
    {
        // Generate a random position within the defined respawn area
        Vector3 randomPosition = new Vector3(
            Random.Range(respawnAreaMin.x, respawnAreaMax.x),
            Random.Range(respawnAreaMin.y, respawnAreaMax.y),
            Random.Range(respawnAreaMin.z, respawnAreaMax.z)
        );

        transform.position = randomPosition; // Move the item to the new position
        gameObject.SetActive(true); // Show the object again
        isCollected = false; // Reset the collection state
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a wire cube in the Editor to visualize the respawn area
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube((respawnAreaMin + respawnAreaMax) / 2, respawnAreaMax - respawnAreaMin);
    }
}
