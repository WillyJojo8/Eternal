using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static PlayerStats Instance { get; private set; }

    public int maxHealth = 100;
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
        currentHealth = maxHealth;
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

        // 1) Detener cualquier velocidad residual
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero;

            // 2) Detener la simulación física por completo
            rb.simulated = false;

            // Alternativa: congelar transform
            // rb.constraints = RigidbodyConstraints2D.FreezeAll;

            // O cambiar a estático:
            // rb.bodyType = RigidbodyType2D.Static;
        }

        // 3) Desactivar collider para que no siga colisionando
        Collider2D col = GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
    }

     public void Respawn()
    {
        // Reactivar física y collider
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = true;
        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = true;

        // Restaurar salud
        isDead = false;
        currentHealth = maxHealth;
        healthBar.SetHealth(currentHealth, maxHealth);

    }
}