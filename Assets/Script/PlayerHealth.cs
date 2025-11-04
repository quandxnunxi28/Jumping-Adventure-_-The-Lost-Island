using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;

    public GameObject deathEffect;
    private Animator myAnim;

    public PlayerHealthBar healthBar; // 🔹 Tham chiếu UI

    public AudioClip hurtSound;
    private AudioSource audioSource;

    private void Start()
    {
        myAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("AudioSource chưa được gắn trên Heka! Script sẽ thêm tự động.");
            audioSource = gameObject.AddComponent<AudioSource>(); // tự thêm component
        }

        health = maxHealth;

        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log("Player Health: " + health);
        audioSource.clip = hurtSound;
        audioSource.Play();
        myAnim.SetTrigger("isHurt");

        // 🔹 Cập nhật thanh máu
        if (healthBar != null)
            healthBar.SetHealth(health, maxHealth);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    
    
        Time.timeScale = 1f; // reset time
        SceneManager.LoadScene("MainMenu"); // trỏ đúng tên scene
    
    // Hoặc có thể load lại scene nếu muốn:
    // SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

    
}
