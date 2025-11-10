using System.Collections;
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

    private SpriteRenderer spriteRenderer;
    public float hurtFlashTime = 0.15f;


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
        spriteRenderer = GetComponent<SpriteRenderer>();

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
        StartCoroutine(HurtEffect());

        // 🔹 Cập nhật thanh máu
        if (healthBar != null)
            healthBar.SetHealth(health, maxHealth);

        if (health <= 0)
        {
            Die();
        }
    }

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
    void Die()
    {
        if (deathEffect != null)
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        Destroy(gameObject);
    
    
        Time.timeScale = 1f; // reset time
        //SceneManager.LoadScene("MainMenu"); // trỏ đúng tên scene

        // Hoặc có thể load lại scene nếu muốn:
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("vuc"))
        {
            Die();
        }
    }


}
