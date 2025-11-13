using UnityEngine;
using UnityEngine.UI;

public class PlayerManaBar : MonoBehaviour
{
    public Image fillImage;

    public void SetMaxMana(int health)
    {
        fillImage.fillAmount = 1f;
    }

    public void SetMana(int currentMana, int maxMana)
    {
        fillImage.fillAmount = (float)currentMana / maxMana;
    }
}
