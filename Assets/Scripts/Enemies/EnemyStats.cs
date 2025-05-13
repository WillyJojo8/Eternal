using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;

    [Header("Curación")]
    public GameObject healingButtonPrefab;
    [Range(0f, 1f)] public float dropChance = 0.1f; // 10%

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
        // Añadir al contador del GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddEnemyKill();
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