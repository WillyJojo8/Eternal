using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AbilitiesUI : MonoBehaviour
{
    [Header("UI: Contador")]
    public TextMeshProUGUI buttonCountText;

    [Header("Velocidad")]
    public TextMeshProUGUI speedLevelText;
    public TextMeshProUGUI speedCostText;

    [Header("Salud")]
    public TextMeshProUGUI healthLevelText;
    public TextMeshProUGUI healthCostText;

    void Start()
    {
        // Verifica que todo esté asignado
        if (GameManager.Instance == null)
        {
            Debug.LogError("❌ GameManager.Instance es null.");
            return;
        }

        if (buttonCountText == null || speedLevelText == null || speedCostText == null || healthLevelText == null || healthCostText == null)
        {
            Debug.LogError("❌ Uno o más campos de UI no están asignados en el Inspector.");
            return;
        }

        UpdateUI();
    }

    void Update()
    {
        // Tecla de prueba para sumar botones
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameManager.Instance.AddButtons(1000);
            Debug.Log("🟢 Añadidos 1000 botones de prueba");
            UpdateUI();
        }
    }

    void UpdateUI()
    {
        if (GameManager.Instance == null) return;

        int botones = GameManager.Instance.TotalButtons;
        int speedLevel = GameManager.Instance.SpeedLevel;
        int healthLevel = GameManager.Instance.HealthLevel;

        buttonCountText.text = $"Botones: {botones}";
        speedLevelText.text = $"Velocidad (Nivel {speedLevel})";
        healthLevelText.text = $"Salud (Nivel {healthLevel})";

        speedCostText.text = $"Coste: {GameManager.Instance.GetUpgradeCost(speedLevel)}";
        healthCostText.text = $"Coste: {GameManager.Instance.GetUpgradeCost(healthLevel)}";
    }

    public void OnUpgradeSpeed()
    {
        GameManager.Instance.UpgradeSpeed();
        UpdateUI();
    }

    public void OnUpgradeHealth()
    {
        GameManager.Instance.UpgradeHealth();
        UpdateUI();
    }

    public void OnBackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}