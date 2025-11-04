using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Image fillImage;          // Thanh máu màu
    public Transform target;         // Vật thể cần theo dõi (Enemy hoặc Boss)
    public Vector3 offset;           // Độ lệch trên đầu nhân vật

    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Luôn hướng theo camera và nằm đúng vị trí trên đầu
            transform.position = target.position + offset;
            transform.rotation = Quaternion.identity; // Không xoay theo nhân vật
        }
    }

    public void SetHealth(float current, float max)
    {
        fillImage.fillAmount = current / max;
    }
}
