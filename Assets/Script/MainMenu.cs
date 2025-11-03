using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Chuyển sang map kakaka
        SceneManager.LoadScene("kakaka");
    }

    public void OpenOptions()
    {
        Debug.Log("Options clicked!");
    }

    public void ExitGame()
    {
        Debug.Log("Exit clicked!");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
