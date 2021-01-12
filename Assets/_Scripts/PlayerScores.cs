using TMPro;
using UnityEngine;

public class PlayerScores : MonoBehaviour
{
    public int PlayerID { get; set; }

    [SerializeField]
    private TextMeshProUGUI KillsTxt;

    [SerializeField]
    private TextMeshProUGUI DeathsTxt;

    [SerializeField]
    private TextMeshProUGUI PlayerNameTxt;

    public void SetPlayerNameInLabel(string playerName)
    {
        PlayerNameTxt.text = playerName;

    }

    public void UpdateScoresDisplay(int kills, int deaths)
    {
        KillsTxt.text = kills.ToString();
        DeathsTxt.text = deaths.ToString();
    }
}