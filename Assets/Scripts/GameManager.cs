using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool HasSelectedPerk { get; private set; } = false;

    [Header("Enemy Count")]
    public TextMeshProUGUI enemyCountText;
    private int enemyCount = 0;

    [Header("Progreso / Botones")]
    public TextMeshProUGUI buttonCountText;
    private int totalButtons = 0;

    // Propiedades públicas
    public int TotalButtons => totalButtons;
    public int SpeedLevel => PlayerPrefs.GetInt("SpeedLevel", 0);
    public int HealthLevel => PlayerPrefs.GetInt("HealthLevel", 0);


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Reasignar referencias si se perdieron entre escenas
        if (buttonCountText == null)
        {
            var found = GameObject.Find("ButtonCountText");
            if (found != null)
                buttonCountText = found.GetComponent<TextMeshProUGUI>();
        }

        if (enemyCountText == null)
        {
            var foundEnemy = GameObject.Find("EnemyCountText");
            if (foundEnemy != null)
                enemyCountText = foundEnemy.GetComponent<TextMeshProUGUI>();
        }

        UpdateButtonUI();
        UpdateEnemyCountUI();
    }

    public void ConfirmPerkSelected()
    {
        HasSelectedPerk = true;
    }

    public void ResetGameState()
    {
        HasSelectedPerk = false;
        enemyCount = 0;
        UpdateEnemyCountUI();
        UpdateButtonUI();
    }

    // ========== ENEMIGOS ==========
    public void AddEnemyKill()
    {
        enemyCount++;
        UpdateEnemyCountUI();
    }

    private void UpdateEnemyCountUI()
    {
        if (enemyCountText != null)
        {
            enemyCountText.text = "Enemy count: " + enemyCount;
        }
    }

    // ========== BOTONES ==========
    public void AddButtons(int amount)
    {
        totalButtons += amount;
        SaveProgress();
        UpdateButtonUI();
    }

    public void SpendButtons(int amount)
    {
        totalButtons -= amount;
        if (totalButtons < 0) totalButtons = 0;
        SaveProgress();
        UpdateButtonUI();
    }

    private void UpdateButtonUI()
    {
        if (buttonCountText != null)
        {
            buttonCountText.text = "Botones: " + totalButtons;
        }
    }

    // ========== MEJORAS ==========
    public float GetPlayerSpeedBonus() => SpeedLevel * 0.5f;
    public int GetPlayerHealthBonus() => HealthLevel * 20;

    public void UpgradeSpeed()
    {
        int cost = GetUpgradeCost(SpeedLevel);
        if (totalButtons >= cost)
        {
            SpendButtons(cost);
            PlayerPrefs.SetInt("SpeedLevel", SpeedLevel + 1);
            SaveProgress();
        }
    }

    public void UpgradeHealth()
    {
        int cost = GetUpgradeCost(HealthLevel);
        if (totalButtons >= cost)
        {
            SpendButtons(cost);
            PlayerPrefs.SetInt("HealthLevel", HealthLevel + 1);
            SaveProgress();
        }
    }

    public int GetUpgradeCost(int currentLevel)
    {
        // Fórmula: 100 * (2 ^ nivel) - 100
        return Mathf.RoundToInt(100 * Mathf.Pow(2f, currentLevel)) - 100;
    }

    // ========== GUARDADO ==========
    private void SaveProgress()
    {
        PlayerPrefs.SetInt("TotalButtons", totalButtons);
        PlayerPrefs.Save();
    }

    private void LoadProgress()
    {
        totalButtons = PlayerPrefs.GetInt("TotalButtons", 0);
        UpdateButtonUI();
    }

    [ContextMenu("Resetear progreso")]
    public void ResetAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("🎯 Progreso reseteado correctamente.");
    }

}