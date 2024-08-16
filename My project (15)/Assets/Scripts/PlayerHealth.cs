using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Transform respawnPoint; // The position where the player will respawn

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player Died");
        Respawn(); // Respawn the player
    }

    private void Respawn()
    {
        transform.position = respawnPoint.position; // Move the player to the respawn point
        currentHealth = maxHealth; // Reset health to max
        Debug.Log("Player Respawned");
    }
}
