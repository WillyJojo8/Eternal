using UnityEngine;

public class EnsureGameManager : MonoBehaviour
{
    public GameObject gameManagerPrefab;

    void Awake()
    {
        if (GameManager.Instance == null)
        {
            Instantiate(gameManagerPrefab);
        }
    }
}