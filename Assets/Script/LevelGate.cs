using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGate : MonoBehaviour
{
    public AudioClip gateSound; // optional âm thanh khi chạm cổng
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            
            PlayerStats.Instance.health += 50;
            PlayerStats.Instance.mana +=50;
            if(PlayerStats.Instance.health >= PlayerStats.Instance.maxhealth)
            {
                PlayerStats.Instance.health = PlayerStats.Instance.maxhealth;
            }
            if (PlayerStats.Instance.mana >= PlayerStats.Instance.maxMana)
            {
                PlayerStats.Instance.mana = PlayerStats.Instance.maxMana;
            }
            // Play sound cổng
            if (gateSound != null)
                audioSource.PlayOneShot(gateSound);

            // Load level tiếp theo
            int currentIndex = SceneManager.GetActiveScene().buildIndex;
            int totalScenes = SceneManager.sceneCountInBuildSettings;

            if (currentIndex + 1 < totalScenes)
                SceneManager.LoadScene(currentIndex + 1);
            else
                SceneManager.LoadScene(0); // quay về menu hoặc level 1
        }
    }
}
