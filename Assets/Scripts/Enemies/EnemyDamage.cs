using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    public int damage = 10;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}