using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject[] pressurePlates;
    public GameObject door;

    public bool IsActivated { get; private set; }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Open the door or reveal the object
            door.SetActive(false); // Hides the door, simulating opening
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Close the door when the player steps off
            door.SetActive(true); // Shows the door again
        }
    }
    void Update()
    {
        if (AllPressurePlatesActivated())
        {
            // Open the door if all plates are activated
            door.SetActive(false);
        }
    }
    bool AllPressurePlatesActivated()
{
    foreach (var plate in pressurePlates)
    {
        if (!plate.GetComponent<PressurePlate>().IsActivated)
        {
            return false;
        }
    }
    return true;
}
}