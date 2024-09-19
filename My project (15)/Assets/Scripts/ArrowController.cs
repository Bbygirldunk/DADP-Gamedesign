using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    // Reference to the arrow GameObject
    private GameObject arrowObject;

    void Start()
    {
        arrowObject = gameObject; // Assign the arrow itself (this script is attached to the arrow GameObject)
        arrowObject.SetActive(false); // Arrow starts disabled (invisible)
    }

    // Function to show the arrow (activate the GameObject)
    public void ShowArrow()
    {
        arrowObject.SetActive(true); // Enable the arrow GameObject
        Invoke("HideArrow", 5f); // Automatically hide the arrow after 5 seconds (adjust time as needed)
    }

    // Function to hide the arrow (deactivate the GameObject)
    void HideArrow()
    {
        arrowObject.SetActive(false); // Disable the arrow GameObject
    }
}
