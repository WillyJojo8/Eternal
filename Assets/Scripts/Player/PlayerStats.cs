using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public int baseHealth = 100;
    private int maxHealth;
    private int currentHealth;

    public bool isDead { get; private set; }

    public HealthBarUI_Image healthBar;

    [Header("Perks")]
    public bool hasFreezingShot = false;
    public bool hasPoisonedShot = false;
    public bool hasExplosiveShot = false;
    public bool hasPiercingShot = false;
    public bool hasSpreadShot = false;

    [HideInInspector] public bool hasSelectedPerk = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        GameManager.Instance.ResetGameState();
        isDead = false;
        hasSelectedPerk = false;

        maxHealth = baseHealth + GameManager.Instance.GetPlayerHealthBonus();
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);

        if (PerkManager.Instance != null && PerkSelectionUI.Instance != null)
        {
            var perks = PerkManager.Instance.GetRandomPerks(2);
            PerkSelectionUI.Instance.Show(perks);
            Debug.Log("Mostrando perks iniciales.");
        }
        else
        {
            Debug.LogWarning("PerkManager o PerkSelectionUI no están disponibles.");
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        if (healthBar != null)
            healthBar.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (isDead) return;

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        if (healthBar != null)
            healthBar.SetHealth(currentHealth, maxHealth);
    }

    void Die()
    {
        isDead = true;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }

        Debug.Log("Player died");

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.simulated = false;
        }

        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        // 👉 AÑADE ESTA LÍNEA
        GameManager.Instance.GuardarBotonesAlMorir();

        Invoke("ReturnToMainMenu", 2f);
    }

    void ReturnToMainMenu()
    {
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Player"))
            Destroy(obj);

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("MainCamera"))
            Destroy(obj);

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("GameUI"))
            Destroy(obj);

        SceneManager.LoadScene("MainMenu");
    }

    public void Respawn()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = true;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = true;

        isDead = false;

        maxHealth = baseHealth + GameManager.Instance.GetPlayerHealthBonus();
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth, maxHealth);
    }
}