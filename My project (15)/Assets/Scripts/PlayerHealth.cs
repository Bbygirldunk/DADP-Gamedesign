using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 10f;
    private float currentHealth;

    public GameObject respawnPoint;  // The position where the player will respawn
    public GameObject Player;
    private bool isDead = false;    // To prevent multiple respawn calls

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        if (!isDead)
        {
            currentHealth -= damageAmount;
            Debug.Log("Player Health: " + currentHealth);

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log("Player Died");
        isDead = true;
        StartCoroutine(RestartGame());
        //Respawn();  // Respawn the player
    }
    
    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

  /*  private void Respawn()
    {
        Player.transform.position = respawnPoint.transform.position;  // Move the player to the respawn point
        currentHealth = maxHealth;  // Reset health to max
        isDead = false;  // Allow damage again
        Debug.Log("Player Respawned");
    }
}*/
