using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneCleaner : MonoBehaviour
{
    void Start()
    {
        string currentScene = SceneManager.GetActiveScene().name;

        // Esperar a que GameManager esté listo
        if (GameManager.Instance == null)
        {
            Debug.LogWarning("⚠️ GameManager.Instance no está disponible todavía. Abortando limpieza.");
            return;
        }

        foreach (GameObject obj in FindObjectsOfType<GameObject>())
        {
            // No destruir el GameManager
            if (obj == GameManager.Instance.gameObject)
                continue;

            // Solo destruir los que no pertenecen a esta escena
            if (!obj.scene.name.Equals(currentScene))
            {
                Debug.Log($"🧹 Eliminando persistente no deseado: {obj.name}");
                Destroy(obj);
            }
        }
    }
}