using UnityEngine;

[CreateAssetMenu(fileName = "newPerk", menuName = "Game/Perk")]
public class Perk : ScriptableObject
{
    public string perkName;
    public Sprite icon;
    public string description;

    // Lógica que se ejecuta cuando el jugador lo elige
    public virtual void Apply(GameObject player)
    {
        Debug.Log($"Perk {perkName} aplicado a {player.name}");
    }
}
