using UnityEngine;

[CreateAssetMenu(fileName = "PoisonedShot", menuName = "Game/Perks/PoisonedShot")]
public class PoisonedShotPerk : Perk
{
    public override void Apply(GameObject player)
    {
        base.Apply(player); // Log estándar

        Debug.Log("✅ Activando perk: PoisonedShot");

        var stats = player.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.hasPoisonedShot = true;
        }
    }

}
