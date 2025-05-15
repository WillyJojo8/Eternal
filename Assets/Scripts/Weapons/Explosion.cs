using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour
{
    private float radius;
    private int damage;

    public void Init(float r, int d)
    {
        radius = r;
        damage = d;
        StartCoroutine(DoExplosion());
    }

    private IEnumerator DoExplosion()
    {
        // 1) Aplica daño inmediatamente
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, radius);
        foreach (var hit in hits)
        {
            if (hit.CompareTag("Enemy"))
            {
                hit.GetComponent<EnemyStats>()?.TakeDamage(damage);
            }
        }

        // 2) Espera a que acabe la partícula
        yield return new WaitForSeconds(0.5f);

        // 3) Destruye el objeto completo (partícula + script)
        Destroy(gameObject);
    }

    #if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
    #endif
}
