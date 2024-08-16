using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 20f;
    public float lifetime = 3f;

    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the bullet after its lifetime expires
    }
  private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        //Destroy(gameObject); // Destroy the bullet after hitting something
    }
}
  
