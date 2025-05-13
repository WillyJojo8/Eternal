using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance { get; private set; }

    public int baseHealth = 100;  // Salud base sin mejoras
    private int maxHealth;
    private int currentHealth;

    public bool isDead { get; private set; }

    public HealthBarUI_Image healthBar;

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

        // Calcular salud total con mejora permanente
        maxHealth = baseHealth + GameManager.Instance.GetPlayerHealthBonus();
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.SetMaxHealth(maxHealth);

        // Mostrar perks iniciales
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

        // Eliminar todos los enemigos
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(enemy);
        }

        Debug.Log("Player died");

        // Detener física
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;
            rb.simulated = false;
        }

        // Desactivar colisión
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;

        // Redirigir al menú principal después de un pequeño retardo
        Invoke("ReturnToMainMenu", 2f); // 2 segundos para que se vea que ha muerto
    }

    void ReturnToMainMenu()
    {
        // Limpiar objetos sobrantes que no deben pasar al menú
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

        // Restaurar salud (aplicando de nuevo mejora si se respawnea en algún otro modo)
        maxHealth = baseHealth + GameManager.Instance.GetPlayerHealthBonus();
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.SetHealth(currentHealth, maxHealth);
    }
}