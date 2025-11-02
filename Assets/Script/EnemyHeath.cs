using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeath : MonoBehaviour
{
 
    public int health = 200;

    public GameObject deathEffect;

    public bool isInvulnerable = false;

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
