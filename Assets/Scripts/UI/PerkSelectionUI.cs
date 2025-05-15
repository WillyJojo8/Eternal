using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PerkSelectionUI : MonoBehaviour
{
    public static PerkSelectionUI Instance { get; private set; }

    [Header("UI Elements")]
    public GameObject panel;
    public Button[] optionButtons;

    private List<Perk> currentPerks;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        panel.SetActive(false);
    }

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

                var img = btn.transform.Find("Icon")?.GetComponent<Image>();
                img.sprite = perks[i].icon;

                var txt = btn.GetComponentInChildren<TextMeshProUGUI>();
                if (txt != null)
                    txt.text = perks[i].perkName;
                else
                    Debug.LogWarning($"Button '{btn.name}' no tiene TextMeshProUGUI hijo");

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
        var chosen = currentPerks[index];
        chosen.Apply(PlayerStats.Instance.gameObject);

        // ✅ Activar perks
        PlayerStats.Instance.hasSelectedPerk = true;

        panel.SetActive(false);
        GameManager.Instance.ConfirmPerkSelected();
        PlayerStats.Instance.Respawn();
    }
}