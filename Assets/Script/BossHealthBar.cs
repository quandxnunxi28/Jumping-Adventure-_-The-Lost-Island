using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Image fillImage;           // Phần màu đỏ (máu)
    public GameObject uiBossHealthBar; // GameObject chứa toàn bộ thanh máu

    public void SetMaxHealth(int health)
    {
        fillImage.fillAmount = 1f;
    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        fillImage.fillAmount = (float)currentHealth / maxHealth;
    }

    public void Show()
    {
        uiBossHealthBar.SetActive(true);
    }

    public void Hide()
    {
        uiBossHealthBar.SetActive(false);
    }
}
