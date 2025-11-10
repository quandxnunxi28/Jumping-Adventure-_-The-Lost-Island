using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss3_Health : MonoBehaviour
{
    public int maxHealth = 500;
    public int health;

    public GameObject deathEffect;
    public bool isInvulnerable = false;
    public GameObject door;

    // 🔹 Tham chiếu đến 2 con quái bảo vệ
    [Header("Guard Minions")]
    public GameObject minion1;
    public GameObject minion2;

    // 🔹 Tham chiếu đến script thanh máu (gắn trên UI)
    public BossHealthBar healthBar;

    private SpriteRenderer spriteRenderer;
    public float hurtFlashTime = 0.15f;

    void Start()
    {
        health = maxHealth;

        if (healthBar != null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            healthBar.Hide();
            healthBar.SetMaxHealth(maxHealth);
        }

        // Khi bắt đầu, boss luôn bất tử cho đến khi 2 minion chết
        isInvulnerable = true;
    }

    public void TakeDamage(int damage)
    {
        // 🔸 Kiểm tra xem 2 con minion còn sống không
        CheckMinionsAlive();

        // Nếu boss vẫn bất tử, return ngay
        if (isInvulnerable)
            return;

        StartCoroutine(HurtEffect());

        health -= damage;

        if (healthBar != null)
        {
            healthBar.SetHealth(health, maxHealth);
        }

        if (health <= 0)
        {
            Die();
        }
    }

    void CheckMinionsAlive()
    {
        bool minion1Alive = (minion1 != null);
        bool minion2Alive = (minion2 != null);

        // 🔹 Nếu cả 2 đều chết -> boss có thể bị đánh
        if (!minion1Alive && !minion2Alive)
        {
            isInvulnerable = false;
            Debug.Log("Boss is now vulnerable!");
        }
        else
        {
            isInvulnerable = true;
        }
    }

    IEnumerator HurtEffect()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            yield return new WaitForSeconds(hurtFlashTime);
            spriteRenderer.color = Color.white;
        }
    }

    void Die()
    {
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        if (healthBar != null)
            healthBar.Hide();

        Destroy(gameObject);
        Instantiate(door, transform.position, Quaternion.identity);
    }
}
