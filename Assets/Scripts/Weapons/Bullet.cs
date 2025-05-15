using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public Vector2 direction;

    void Start()
    {
        Destroy(gameObject, 2f);
    }

    void Update()
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        EnemyStats enemy = other.GetComponent<EnemyStats>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);

            // Freeze y Poison ya funcionan igual que antes…
            if (PlayerStats.Instance.hasFreezingShot)
                enemy.ApplyFreeze(2f);

            if (PlayerStats.Instance.hasPoisonedShot)
                enemy.ApplyPoison(5, 2, 1f);

            // Explosive Shot:
            if (PlayerStats.Instance.hasExplosiveShot)
                Explode();
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        // Obtenemos el perk que guardamos en Current
        var perk = ExplosiveShotPerk.Current;
        if (perk == null) return;  // por si acaso

        // Instanciamos el prefab y le pasamos los valores
        GameObject expGO = Instantiate(perk.explosionPrefab,
                                       transform.position,
                                       Quaternion.identity);
        var eScript = expGO.GetComponent<Explosion>();
        if (eScript != null)
            eScript.Init(perk.explosionRadius,
                         perk.explosionDamage);
    }
}
