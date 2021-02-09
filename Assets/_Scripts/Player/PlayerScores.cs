using TMPro;
using UnityEngine;

public class PlayerScores : MonoBehaviour
{
    public int PlayerID { get; set; }

    [SerializeField]
    private TextMeshProUGUI _killsTxt;

    [SerializeField]
    private TextMeshProUGUI _deathsTxt;

    [SerializeField]
    private TextMeshProUGUI _playerNameTxt;

    public int Kills = 0;
    public int Deaths = 0;

    public void SetPlayerNameInLabel(string playerName)
    {
        _playerNameTxt.text = playerName;
    }

    public void UpdateScoresDisplay(int kills, int deaths)
    {
        Kills = kills;
        Deaths = deaths;
        _killsTxt.text = kills.ToString();
        _deathsTxt.text = deaths.ToString();
    }
}