using UnityEngine;

[CreateAssetMenu(fileName = "FreezingShot", menuName = "Game/Perks/FreezingShot")]
public class FreezingShotPerk : Perk
{
    public override void Apply(GameObject player)
    {
        base.Apply(player); // Log estándar

        Debug.Log("✅ Activando perk: FreezingShot");

        var stats = player.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.hasFreezingShot = true;
        }
    }

}
