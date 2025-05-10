using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PerkManager : MonoBehaviour
{
    public static PerkManager Instance { get; private set; }

    [Tooltip("Lista de todos los perks posibles en el juego")]
    public List<Perk> allPerks;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    /// <summary>Devuelve N perks distintos al azar.</summary>
    public List<Perk> GetRandomPerks(int count)
    {
        return allPerks
            .OrderBy(x => Random.value)
            .Take(count)
            .ToList();
    }
}
