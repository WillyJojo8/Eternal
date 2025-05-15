using UnityEngine;

[CreateAssetMenu(menuName = "Game/Perks/ExplosiveShot")]
public class ExplosiveShotPerk : Perk
{
    // ← Nuevo: referencia al perk activo
    public static ExplosiveShotPerk Current { get; private set; }

    public GameObject explosionPrefab;
    public float explosionRadius = 2f;
    public int explosionDamage = 30;

    public override void Apply(GameObject player)
    {
        base.Apply(player);

        var stats = player.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.hasExplosiveShot = true;
            // ← Guardamos esta instancia como la “actual”
            Current = this;
        }
    }
}
