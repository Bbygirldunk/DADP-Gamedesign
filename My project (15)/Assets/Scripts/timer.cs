using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;
    [SerializeField] GameObject gameOverUI; // Reference to your Game Over UI screen

    private bool gameOverTriggered = false;

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;

            // Calculate minutes and seconds
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);

            // Update the timer text
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else if (!gameOverTriggered)
        {
            remainingTime = 0;
            TriggerGameOver();
        }
    }

    private void TriggerGameOver()
    {
        gameOverTriggered = true;

        // Display the "Game Over" UI screen
        if (gameOverUI != null)
        {
            gameOverUI.SetActive(true);
        }

        // Optionally disable player controls
        // Example: GetComponent<PlayerController>().enabled = false;

        Debug.Log("Game Over!");

        // Exit the game
        QuitGame();
    }

    private void QuitGame()
    {
        // Check if the game is running in the Unity editor or a built version
#if UNITY_EDITOR
        // Stop the game in the editor
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Quit the game in the built version
        Application.Quit();
#endif
    }
}



