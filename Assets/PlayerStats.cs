using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public int health = 100;
    public int maxhealth = 100;
    public int mana = 100;
    public int maxMana = 100;
    public int maxAttackDamage = 25;
    public int minAttackDamage = 15;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ lại khi đổi map
        }
        else
        {
            Destroy(gameObject); // Xóa bản trùng
        }
    }
}
