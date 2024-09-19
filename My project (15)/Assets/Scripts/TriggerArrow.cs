using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TriggerArrow : MonoBehaviour
{
    public ArrowController arrow; // Reference to the specific arrow at this location

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure this triggers only when the player enters
        {
            arrow.ShowArrow(); // Call the ShowArrow function to display the arrow
        }
    }
}
