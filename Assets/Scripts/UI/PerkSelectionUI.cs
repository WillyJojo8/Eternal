using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerkSelectionUI : MonoBehaviour
{
    public static PerkSelectionUI Instance { get; private set; }

    [Header("UI Elements")]
    public GameObject panel;              // Tu Panel principal
    public Button[] optionButtons;        // Array con tus botones

    private List<Perk> currentPerks;      // Perks que se están mostrando

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        panel.SetActive(false);
    }

    /// <summary>
    /// Muestra el panel con las opciones de perks.
    /// </summary>
    public void Show(List<Perk> perks)
    {
        currentPerks = perks;
        panel.SetActive(true);

        for (int i = 0; i < optionButtons.Length; i++)
        {
            var btn = optionButtons[i];
            if (i < perks.Count)
            {
                btn.gameObject.SetActive(true);

                // Icono
                var img = btn.transform.Find("Icon")?.GetComponent<Image>();
                if (img != null) img.sprite = perks[i].icon;

                // Texto TMP
                var txt = btn.GetComponentInChildren<TextMeshProUGUI>();
                if (txt != null) 
                    txt.text = perks[i].perkName;
                else 
                    Debug.LogWarning($"Button '{btn.name}' no tiene TextMeshProUGUI hijo");

                // Debug
                Debug.Log($"Perk {perks[i].perkName}");

                // Listener
                btn.onClick.RemoveAllListeners();
                int index = i;
                btn.onClick.AddListener(() => OnOptionSelected(index));
            }
            else
            {
                btn.gameObject.SetActive(false);
            }
        }
    }

    void OnOptionSelected(int index)
    {
        // Aplica el perk
        var chosen = currentPerks[index];

        chosen.Apply(PlayerStats.Instance.gameObject);

        panel.SetActive(false);

        PlayerStats.Instance.Respawn();
    }
}
