using UnityEngine;

[CreateAssetMenu(menuName = "Game/Perks/PiercingShot")]
public class PiercingShotPerk : Perk
{
    
    public static PiercingShotPerk Current { get; private set; }

    [Tooltip("Número máximo de enemigos que atraviesa cada bala")]
    public int pierceCount = 2;

    public override void Apply(GameObject player)
    {
        base.Apply(player);
        var stats = player.GetComponent<PlayerStats>();
        if (stats != null){
            stats.hasPiercingShot = true;
            Current = this;
        }
    }
}
