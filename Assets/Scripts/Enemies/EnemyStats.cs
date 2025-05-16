using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;

    public bool defaultEnemy;

    [Header("Curación")]
    public GameObject healingButtonPrefab;
    [Range(0f, 1f)] public float dropChance = 0.1f;

    [Header("Default enemy")]
    public GameObject defaultEnemyPrefab;

    [Header("Progreso")]
    public int buttonsGiven = 1;

    private bool isFrozen = false;

    [Header("VFX")]
    public GameObject poisonEffectPrefab;

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
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddEnemyKill();
            GameManager.Instance.AddButtons(buttonsGiven);
        }

        TryDropHealing();

        if (!defaultEnemy && defaultEnemyPrefab != null)
        {
            float offset = 1.5f;

            Vector3 basePos = transform.position;
            Quaternion rot = transform.rotation;

            Vector3 posLeft  = basePos + Vector3.left  * offset;
            Vector3 posRight = basePos + Vector3.right * offset;

            Instantiate(defaultEnemyPrefab, posLeft,  rot);
            Instantiate(defaultEnemyPrefab, posRight, rot);
        }
        
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

    // ========== CONGELACIÓN ==========
    public void ApplyFreeze(float duration)
    {
        if (isFrozen) return;

        Debug.Log("🧊 Enemigo congelado por " + duration + "s");
        StartCoroutine(FreezeCoroutine(duration));
    }

    private IEnumerator FreezeCoroutine(float duration)
    {
        isFrozen = true;

        // Desactiva controlador de movimiento
        var controller = GetComponent<EnemyController>();
        if (controller != null)
            controller.enabled = false;

        // Cambiar color visualmente
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.color = Color.cyan;

        yield return new WaitForSeconds(duration);

        // Restaurar movimiento
        if (controller != null)
            controller.enabled = true;

        // Restaurar color
        if (sr != null)
            sr.color = Color.white;

        isFrozen = false;
    }

    // ========== VENENO ==========
    public void ApplyPoison(int damage, int ticks, float interval)
    {
        Debug.Log("☣️ Enemigo envenenado: " + ticks + " ticks de " + damage);
        StartCoroutine(PoisonCoroutine(damage, ticks, interval));
    }

    private IEnumerator PoisonCoroutine(int damage, int ticks, float interval)
    {
        for (int i = 0; i < ticks; i++)
        {
            yield return new WaitForSeconds(interval);

            // Efecto visual
            if (poisonEffectPrefab != null)
            {
                Instantiate(poisonEffectPrefab, transform.position, Quaternion.identity);
            }

            TakeDamage(damage);
        }
    }

}