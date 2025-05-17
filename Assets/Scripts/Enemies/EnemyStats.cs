using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour
{
    [Header("Stats")]
    public int maxHealth = 20;
    private int currentHealth;

    [Header("Default Enemy Spawn")]
    public bool defaultEnemy;
    public GameObject defaultEnemyPrefab;

    [Header("Drops")]
    public GameObject healingButtonPrefab;
    [Range(0f, 1f)] public float dropChance = 0.1f;
    public int buttonsGiven = 1;

    [Header("VFX")]
    public GameObject poisonEffectPrefab;

    // Congelación
    private bool isFrozen = false;
    private SpriteRenderer sr;
    private Color originalColor;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            originalColor = sr.color;
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
            Die();
    }

    void Die()
    {
        // Notificar al GameManager
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddEnemyKill();
            GameManager.Instance.AddButtons(buttonsGiven);
        }

        TryDropHealing();

        // Si no es ya un defaultEnemy, lo reemplazamos por dos
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
        if (Random.value <= dropChance && healingButtonPrefab != null)
            Instantiate(healingButtonPrefab, transform.position, Quaternion.identity);
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

        // Detener movimiento
        var controller = GetComponent<EnemyController>();
        if (controller != null)
            controller.enabled = false;

        // Aplicar color de congelación (azul frío)
        if (sr != null)
            sr.color = new Color(0.4f, 0.6f, 1f, 1f);

        yield return new WaitForSeconds(duration);

        // Restaurar movimiento
        if (controller != null)
            controller.enabled = true;

        // Restaurar color original
        if (sr != null)
            sr.color = originalColor;

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
                Instantiate(poisonEffectPrefab, transform.position, Quaternion.identity);

            TakeDamage(damage);
        }
    }
}
