using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); // Cambia si tu escena se llama distinto
    }

    public void OpenAbilities()
    {
        // En el futuro puedes abrir una escena o panel
        Debug.Log("Abrir pantalla de habilidades (no implementado)");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}