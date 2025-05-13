using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;

    [Header("Curación")]
    public GameObject healingButtonPrefab;
    [Range(0f, 1f)] public float dropChance = 0.1f; // 10%

    [Header("Progreso")]
    public int buttonsGiven = 1; // Puntos de habilidad que da este enemigo

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Añadir al contador y botones del GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddEnemyKill();
            GameManager.Instance.AddButtons(buttonsGiven);
        }

        TryDropHealing();
        Destroy(gameObject);
    }

    void TryDropHealing()
    {
        float chance = Random.Range(0f, 1f);
        if (chance <= dropChance)
        {
            Instantiate(healingButtonPrefab, transform.position, Quaternion.identity);
        }
    }
}