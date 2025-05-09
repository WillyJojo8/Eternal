using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI_Image : MonoBehaviour
{
    public Image fillImage;

    public void SetMaxHealth(int maxHealth)
    {
        fillImage.fillAmount = 1f; // llena al 100%
    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        fillImage.fillAmount = (float)currentHealth / maxHealth;
    }
}