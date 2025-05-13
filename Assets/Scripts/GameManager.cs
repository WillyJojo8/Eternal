using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool HasSelectedPerk { get; private set; } = false;

    [Header("Enemy Count")]
    public TextMeshProUGUI enemyCountText;
    private int enemyCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
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
    }

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
}