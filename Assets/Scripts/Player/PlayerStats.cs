using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    public HealthBarUI_Image healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthBar.SetHealth(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        if (healthBar != null)
            healthBar.SetHealth(currentHealth, maxHealth);
    }


    void Die()
    {
        Debug.Log("Player died");
    }
}