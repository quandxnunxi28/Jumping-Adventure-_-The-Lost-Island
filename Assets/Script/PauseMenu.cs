using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;

    void Update()
    {
        // Bắt phím ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC pressed"); // Kiểm tra phím có hoạt động không
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Debug.Log("Game resumed");
    }
    public void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Debug.Log("Game paused");
    }

    public void Restart()
    {
        PlayerStats.Instance.health = 100;
        PlayerStats.Instance.mana = 100;
        PlayerStats.Instance.minAttackDamage = 15;
        PlayerStats.Instance.maxAttackDamage = 25;
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // ✅ Nút "Quit Game" – quay lại Main Menu
    public void QuitGame()
    {
        Time.timeScale = 1f; // reset time
        SceneManager.LoadScene("MainMenu"); // trỏ đúng tên scene
    }
}
