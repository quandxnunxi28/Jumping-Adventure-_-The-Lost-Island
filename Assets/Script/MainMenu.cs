using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Chuyển sang map kakaka
        SceneManager.LoadScene("map1");
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
