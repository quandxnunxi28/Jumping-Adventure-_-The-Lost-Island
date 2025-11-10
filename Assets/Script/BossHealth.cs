using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 500;
    public int health;

    public GameObject deathEffect;
    public bool isInvulnerable = false;
    public GameObject door;

    // 🔹 Tham chiếu đến script thanh máu (gắn trên UI)
    public BossHealthBar healthBar;
    private Animator myAnim;
    private bool isHurt =false;

    void Start()
    {
        health = maxHealth;

        // Hiển thị thanh máu và đặt giá trị ban đầu
        if (healthBar != null)
        {

            healthBar.Hide();
            healthBar.SetMaxHealth(maxHealth);
            myAnim = GetComponent<Animator>();
        }
    }

    public void TakeDamage(int damage)
    {
        isHurt = true;
        if (isInvulnerable)
            return;
       

        health -= damage;
        // Cập nhật thanh máu UI
        if (healthBar != null)
        {
            healthBar.SetHealth(health, maxHealth);
        }

        // Khi boss chết
        if (health <= 0)
        {
            Die();
        }
    

    }


    void Die()
    {
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);
            


        // Ẩn thanh máu khi boss chết
        if (healthBar != null)
        {
            healthBar.Hide();
        }

        Destroy(gameObject);

        Instantiate(door, transform.position, Quaternion.identity);
    }

}
