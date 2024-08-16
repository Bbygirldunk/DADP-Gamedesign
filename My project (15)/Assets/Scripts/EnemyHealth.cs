using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Speed of the enemy
    public float damageAmount = 20f; // Amount of damage the enemy deals to the player
    public float chaseRange = 10f; // Range within which the enemy starts chasing the player

    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player by tag
    }

    void Update()
    {
        // Check the distance between the enemy and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // If the player is within the chase range, move towards the player
        if (distanceToPlayer <= chaseRange)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Optionally, make the enemy face the player
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    internal void TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }
}