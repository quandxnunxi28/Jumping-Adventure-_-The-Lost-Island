using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healAmount = 10; // số máu hồi
    public AudioClip pickupSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Hồi máu cho player
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.health += healAmount;

                // Giới hạn maxHealth
                if (player.health > player.maxHealth)
                    player.health = player.maxHealth;

                // Cập nhật UI health bar
                if (player.healthBar != null)
                    player.healthBar.SetHealth(player.health, player.maxHealth);
            }

            // Play âm thanh nhặt item
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            // Hủy item sau khi nhặt
            Destroy(gameObject);
        }
    }
}
