using UnityEngine;

public class ComicController : MonoBehaviour
{
    public GameObject comicCanvas;
    public GameObject mainMenuCanvas;
    public GameObject videoMenu;

    private int currentPanel = 0;

    public UnityEngine.UI.Image comicImage;
    public Sprite[] panels;

    void Start()
    {
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