using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public int health = 100;

    public GameObject deathEffect;

    private Animator myAnim;

    private void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        myAnim.SetTrigger("isHurt");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        Destroy(gameObject);
    }



}
