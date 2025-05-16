using UnityEngine;
using UnityEngine.UI;
using TMPro;  //  ← necesario para TextMeshProUGUI

public class HealthBarUI_Image : MonoBehaviour
{
    [Header("UI References")]
    public Image fillImage;
    public TextMeshProUGUI healthNumber; // arrástralo en el inspector

    public void SetMaxHealth(int maxHealth)
    {
        fillImage.fillAmount = 1f;
        if (healthNumber != null)
            healthNumber.text = maxHealth.ToString();  // o "maxHealth/maxHealth"
    }

    public void SetHealth(int currentHealth, int maxHealth)
    {
        fillImage.fillAmount = (float)currentHealth / maxHealth;
        if (healthNumber != null)
            healthNumber.text = currentHealth.ToString();
    }
}
