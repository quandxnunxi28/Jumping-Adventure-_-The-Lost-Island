using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fill;
    public float health = 100f;

    public void SetHealth(float value)
    {
        health = Mathf.Clamp(value, 0, 100);
        fill.fillAmount = health / 100f;
    }

    void Update()
    {
        // Giảm máu khi nhấn phím Q
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SetHealth(health - 10f);
            Debug.Log("Máu giảm, còn: " + health);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Gọi hàm SetHealth với giá trị máu hiện tại cộng thêm 10
            SetHealth(health + 10f);
            Debug.Log("Nhấn K - Máu tăng, lên: " + health);
        }
    }
}
