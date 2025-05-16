using UnityEngine;

[CreateAssetMenu(menuName = "Game/Perks/SpreadShot")]
public class SpreadShotPerk : Perk
{
    public static SpreadShotPerk Current { get; private set; }

    [Tooltip("Número de proyectiles por disparo")]
    public int pelletCount = 5;

    [Tooltip("Ángulo total del abanico en grados")]
    public float angleSpread = 45f;

    public override void Apply(GameObject player)
    {
        base.Apply(player);

        var stats = player.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.hasSpreadShot = true;
            Current = this;
        }
    }
}
