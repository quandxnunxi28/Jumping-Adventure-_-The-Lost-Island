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
    private SpriteRenderer spriteRenderer;
    public float hurtFlashTime = 0.15f;
    IEnumerator HurtEffect()
    {
        if (spriteRenderer != null)
        {
            // Đổi màu sang đỏ
            spriteRenderer.color = Color.red;

            // Giữ 0.15s rồi trở lại bình thường
            yield return new WaitForSeconds(hurtFlashTime);

            spriteRenderer.color = Color.white;
        }
    }


    void Start()
    {
        health = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Hiển thị thanh máu và đặt giá trị ban đầu
        if (healthBar != null)
        {

            healthBar.Hide();
            healthBar.SetMaxHealth(maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;
        StartCoroutine(HurtEffect());

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
