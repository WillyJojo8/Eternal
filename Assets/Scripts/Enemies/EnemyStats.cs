using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;

    [Header("Curaci�n")]
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
        var ui = FindObjectOfType<EnemyCount_UI>();
        if (ui != null){
            
            ui.IncrementCount();

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