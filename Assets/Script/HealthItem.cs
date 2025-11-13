using UnityEngine;

public class HealthItem : MonoBehaviour
{
    public int healAmount = 20; // số máu hồi
    public int manaAmount = 20;
    public int increaseDamage = 2;
    public AudioClip pickupSound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Hồi máu cho player
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            HeroController playerMana = other.GetComponent<HeroController>();
            MeleeAttack attack = other.GetComponent<MeleeAttack>();
            if (player != null)
            {

                player.health += healAmount;

                // Giới hạn maxHealth
                if (player.health > player.maxHealth)
                    player.health = player.maxHealth;

                // Cập nhật UI health bar
                if (player.healthBar != null)
                    player.healthBar.SetHealth(player.health, player.maxHealth);

                if(player.healthText != null)
                    player.healthText.text = $"{player.health}/{player.maxHealth}";

            }

            if (playerMana != null)
            {
                playerMana.mana += healAmount;

                // Giới hạn maxHealth
                if (playerMana.mana > playerMana.maxMana)
                    playerMana.mana = playerMana.maxMana;

                if(playerMana.manaText != null)
                {
                    playerMana.manaText.text = $"{playerMana.mana}/{playerMana.maxMana}";
                }

                // Cập nhật UI health bar
                if (playerMana.manaBar != null)
                    playerMana.manaBar.SetMana(playerMana.mana, playerMana.maxMana);
            }

            if(attack != null)
            {
                attack.minAttackDamage += increaseDamage;
                attack.maxAttackDamage += increaseDamage;

                attack.atk_Text.text = $"Atk Damage: {(attack.minAttackDamage + attack.maxAttackDamage) / 2}";

            }
            PlayerStats.Instance.health = player.health;
            PlayerStats.Instance.mana = playerMana.mana;
            PlayerStats.Instance.minAttackDamage = attack.minAttackDamage;
            PlayerStats.Instance.maxAttackDamage = attack.maxAttackDamage;


            // Play âm thanh nhặt item
            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            // Hủy item sau khi nhặt
            Destroy(gameObject);
        }
    }
}
