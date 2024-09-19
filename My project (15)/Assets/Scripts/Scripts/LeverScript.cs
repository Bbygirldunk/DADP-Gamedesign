using UnityEngine;
using TMPro;  // Import for TextMeshPro

public class LeverScript : MonoBehaviour
{
    public GameObject door;  // The door object to open
    public TextMeshProUGUI promptText;  // Reference to the UI text for the prompt
    [HideInInspector]
    public bool isActivated = false;  // Make isActivated public so LeverManager can access it

    private Color originalColor;  // Store the original color of the lever

    void Start()
    {
        // Store the original color of the lever
        originalColor = GetComponent<Renderer>().material.color;

        // Ensure prompt is hidden at the start
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isActivated)
        {
            if (promptText != null)
            {
                promptText.gameObject.SetActive(true);
                promptText.text = "Press E to Activate";
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !isActivated && Input.GetKeyDown(KeyCode.E))
        {
            ToggleLever();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (promptText != null)
            {
                promptText.gameObject.SetActive(false);
            }
        }
    }

    public void ToggleLever()
    {
        isActivated = true;  // Set lever to activated state
        GetComponent<Renderer>().material.color = Color.green;  // Change lever color to indicate activation
        if (promptText != null)
        {
            promptText.gameObject.SetActive(false);  // Hide prompt after activation
        }
        Debug.Log("Lever activated!");
    }

    // Function to reset the lever when time runs out
    public void ResetLever()
    {
        isActivated = false;  // Reset activation state
        GetComponent<Renderer>().material.color = originalColor;  // Restore original color
        Debug.Log("Lever reset!");
    }
}
