using UnityEngine;
using TMPro;

public class EnemyCount_UI : MonoBehaviour
{
    [Header("Referencias UI")]
    public TextMeshProUGUI countText; 

    private int enemyCount = 0;

    void Start()
    {
        UpdateCountText();
    }


    public void IncrementCount()
    {
        enemyCount++;
        UpdateCountText();
    }

    private void UpdateCountText()
    {
        if (countText != null)
            countText.text = "Enemy count: "+enemyCount.ToString();
    }
}
