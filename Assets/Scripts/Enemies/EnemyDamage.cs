using UnityEngine;
using System.Collections;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 10;
    public float damageInterval = 1f;

    private Coroutine damageRoutine;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            var playerStats = collision.gameObject.GetComponent<PlayerStats>();
            if (playerStats != null)
            {
                damageRoutine = StartCoroutine(DamageOverTime(playerStats));
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && damageRoutine != null)
        {
            StopCoroutine(damageRoutine);
            damageRoutine = null;
        }
    }

    private IEnumerator DamageOverTime(PlayerStats player)
    {
        while (player != null)
        {
            player.TakeDamage(damage);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
