
using UnityEngine;
using TMPro;
using System.Collections;

public class LeverManager : MonoBehaviour
{
    public GameObject door;  // The door object to open
    public LeverScript[] levers;  // Array to hold all levers
    public float timeLimit = 30f;  // Total time to solve the puzzle
    private float remainingTime;   // Time left
    private bool puzzleSolved = false;  // Track if the puzzle is solved

    public TextMeshProUGUI resetMessage;  // Reference to the Reset UI message
    public float resetMessageDuration = 2f;  // Time to display the reset message

    void Start()
    {
        remainingTime = timeLimit;  // Initialize the timer

        if (resetMessage != null)
        {
            resetMessage.gameObject.SetActive(false);  // Ensure reset message is hidden at start
        }
    }

    void Update()
    {
        if (!puzzleSolved)
        {
            // Countdown the remaining time
            remainingTime -= Time.deltaTime;

            // Check if time has run out
            if (remainingTime <= 0)
            {
                ResetLevers();
                remainingTime = timeLimit;  // Reset the timer for the next attempt
            }

            // Check if all levers are activated
            if (AreAllLeversActivated())
            {
                OpenDoor();
                puzzleSolved = true;
            }
        }
    }

    // Function to check if all levers are activated
    bool AreAllLeversActivated()
    {
        foreach (LeverScript lever in levers)
        {
            if (!lever.isActivated)  // If any lever is not activated, return false
            {
                return false;
            }
        }
        return true;  // All levers are activated
    }

    // Function to open the door
    void OpenDoor()
    {
        if (door != null)
        {
            door.SetActive(false);  // Deactivate the door (simulating it being opened)
        }
        Debug.Log("Puzzle Solved! Door opened.");
    }

    // Function to reset all levers
    void ResetLevers()
    {
        foreach (LeverScript lever in levers)
        {
            lever.ResetLever();  // Call the Reset function in LeverScript
        }
        Debug.Log("Levers reset!");

       
        StartCoroutine(ShowResetMessage());
    }

   
    IEnumerator ShowResetMessage()

    {
        if (resetMessage != null)
        {
            resetMessage.gameObject.SetActive(true);  
            yield return new WaitForSeconds(resetMessageDuration);  
            resetMessage.gameObject.SetActive(false);  
        }
    }
}
