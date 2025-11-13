using UnityEngine;
using UnityEngine.SceneManagement; // Dùng để chuyển Scene
using UnityEngine.Video; // Dùng để làm việc với VideoPlayer

public class IntroScript : MonoBehaviour
{
    // Kéo component VideoPlayer của video intro vào đây trong Inspector
    [Header("Cài đặt Video Intro")]
    public VideoPlayer videoPlayer;

    [Tooltip("Tên Scene sẽ chuyển tới sau khi video kết thúc (ví dụ: MainMenu)")]
    public string nextSceneName = "MainMenu";

    void Start()
    {
        // Kiểm tra xem VideoPlayer đã được gán chưa
        if (videoPlayer == null)
        {
            Debug.LogError("⚠️ Chưa gán Video Player trong Inspector!");
            return;
        }

        // Khi video kết thúc, gọi hàm OnVideoEnd
        videoPlayer.loopPointReached += OnVideoEnd;

        // Nếu video không bật 'Play On Awake', có thể chủ động phát ở đây
        // videoPlayer.Play();
    }

    // Hàm được gọi tự động khi video kết thúc
    void OnVideoEnd(VideoPlayer vp)
    {
        Debug.Log("🎬 Video intro đã phát xong — chuyển sang Scene kế tiếp...");
        SceneManager.LoadScene(nextSceneName);
    }

    // Hủy đăng ký sự kiện khi đối tượng bị xóa
    void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd;
        }
    }
}
