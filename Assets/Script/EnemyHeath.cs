using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHeath : MonoBehaviour
{
    public int maxHealth = 200;
    public int health;

    public GameObject deathEffect;
    public bool isInvulnerable = false;

    public GameObject itemPrefab; // Prefab của vật phẩm
    public float dropChance = 0.5f; // 50% tỉ lệ
    public GameObject damPopUp;

    // Gọi khi quái chết
    public void DropItem()
    {
        float randomValue = Random.Range(0f, 1f); // random từ 0 đến 1

        if (randomValue <= dropChance)
        {
            // Instantiate item tại vị trí quái chết
            Instantiate(itemPrefab, transform.position, Quaternion.identity);
            Debug.Log("Item rớt ra!");
        }
        else
        {
            Debug.Log("Không rớt item.");
        }
    }

    // 🔹 Thêm biến để kết nối thanh máu
    public HealthBar healthBar;

    void Start()
    {
        health = maxHealth;

        // Cập nhật thanh máu ngay khi bắt đầu
        if (healthBar != null)
            healthBar.SetHealth(health, maxHealth);
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable)
            return;

        health -= damage;
        if (health < 0) health = 0;

        if (damPopUp != null)
        {
            GameObject instance = Instantiate(damPopUp, transform.position
                    + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0.5f, 0), Quaternion.identity);
            instance.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
            Animator animator = instance.GetComponentInChildren<Animator>();
            if (damage <= 0) animator.Play("normal");
            else animator.Play("critical");
        }

        // 🔹 Cập nhật thanh máu sau khi nhận damage
        if (healthBar != null)
            healthBar.SetHealth(health, maxHealth);

        if (health <= 0)
        {
            DropItem();
            Die();
        }
    }

    void Die()
    {
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
