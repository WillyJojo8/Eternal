using UnityEngine;

public class ComicController : MonoBehaviour
{
    public GameObject comicCanvas;
    public GameObject mainMenuCanvas;
    public GameObject videoMenu;

    private int currentPanel = 0;
    public UnityEngine.UI.Image comicImage;
    public Sprite[] panels;

    // 👇 Esta variable estática guarda si ya se mostró la cinemática
    private static bool comicAlreadyPlayed = false;

    void Start()
    {
        // 👇 Si ya se mostró, saltamos a MainMenu directamente
        if (comicAlreadyPlayed)
        {
            comicCanvas.SetActive(false);
            mainMenuCanvas.SetActive(true);
            videoMenu.SetActive(true);
            return;
        }

        // 👇 Primera vez: mostrar cinemática y marcarla como ya reproducida
        comicAlreadyPlayed = true;
        ShowPanel(0);
    }

    public void NextPanel()
    {
        currentPanel++;
        if (currentPanel < panels.Length)
        {
            ShowPanel(currentPanel);
        }
        else
        {
            EndComic();
        }
    }

    private void ShowPanel(int index)
    {
        comicImage.sprite = panels[index];
    }

    private void EndComic()
    {
        comicCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
        videoMenu.SetActive(true);
    }
}