using UnityEngine;

public class PoisonGas : MonoBehaviour
{
    public ParticleSystem poisonGasEffect;  // Reference to the poison gas particle system
    public float gasDamage = 10f;  // Damage per second
    public float gasDuration = 10f; // Duration the gas is active
    public float gasCooldown = 5f;
    private bool playerInGas = false;
    private float gasTimer = 0f;

    private PlayerHealth playerHealth;

    private void Start()
    {
        poisonGasEffect.Stop();  // Ensure gas is off initially
    }

    private void Update()
    {
        if (playerInGas)
        {
            gasTimer += Time.deltaTime;

            if (gasTimer < gasDuration)
            {
                playerHealth.TakeDamage(gasDamage * Time.deltaTime);
            }
            else
            {

                poisonGasEffect.Stop();
                gasTimer += Time.deltaTime;

                if (gasTimer >= gasDuration + gasCooldown + gasCooldown)
                {
                    gasTimer = 0f;
                    poisonGasEffect.Play();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInGas = true;
            gasTimer = 0f;
            poisonGasEffect.Play();  // Activate the gas
            playerHealth = other.GetComponent<PlayerHealth>();
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInGas = false;
            //poisonGasEffect.Stop();  // Stop gas effect when player leaves
        }
    }
}
