using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("GameScene"); // Cambia a la escena de la Partida
    }

    public void OpenAbilities()
    {
        SceneManager.LoadScene("AbilitiesScene"); // Cambia a la escena de las Habilidades
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Saliendo del juego...");
    }
}