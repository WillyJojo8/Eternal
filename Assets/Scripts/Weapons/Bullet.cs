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
        Debug.Log("💥 Bala impactó con: " + other.name); // Asegura que entra aquí

        if (other.CompareTag("Enemy"))
        {
            EnemyStats enemy = other.GetComponent<EnemyStats>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("💥 Daño base aplicado: " + damage);

                if (PlayerStats.Instance != null)
                {
                    if (PlayerStats.Instance.hasFreezingShot)
                    {
                        Debug.Log("❄️ Aplicando efecto congelante");
                        enemy.ApplyFreeze(2f);
                    }

                    if (PlayerStats.Instance.hasPoisonedShot)
                    {
                        Debug.Log("☠️ Aplicando veneno");
                        enemy.ApplyPoison(5, 2, 1f);
                    }
                }
            }

            Destroy(gameObject);
        }

    }
}