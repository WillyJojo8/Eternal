using UnityEngine;

public class HealingButton : MonoBehaviour
{
    public int healAmount = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats stats = other.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.Heal(healAmount);
            }

            Destroy(gameObject);
        }
    }
}