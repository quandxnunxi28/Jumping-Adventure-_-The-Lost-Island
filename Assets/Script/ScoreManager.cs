using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;   // kéo ScoreText vào đây trong Inspector
    private int score = 0;

    // Gọi hàm này để cộng điểm
    public void AddScore(int value)
    {
        score += value;
        UpdateUI();
    }

    // Nếu cần set điểm trực tiếp
    public void SetScore(int value)
    {
        score = value;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
        else
            Debug.LogWarning("ScoreManager: scoreText chưa được gán!");
    }

    // Hàm test: nhấn phím T để cộng 10 điểm (dùng để kiểm tra nhanh)
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            AddScore(10);
        }
    }
}
