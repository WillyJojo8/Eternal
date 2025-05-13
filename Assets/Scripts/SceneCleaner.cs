using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCleaner : MonoBehaviour
{
    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            // No destruir el GameManager
            if (obj == GameManager.Instance.gameObject)
                continue;

            // Si el objeto no pertenece a esta escena, elimínalo
            if (!obj.scene.name.Equals(currentScene))
            {
                Debug.Log($"🧹 Eliminando objeto persistente: {obj.name}");
                Destroy(obj);
            }
        }
    }
}