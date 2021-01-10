using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField]
    private GameObject _playerScoresPrefab;

    [SerializeField]
    private GameObject _scoreBoard;

    private List<PlayerScores> _playersScores = new List<PlayerScores>();

    private void Start()
    {
        if (PhotonNetwork.IsConnected)
        {
            foreach (var player in PhotonNetwork.PlayerList)
            {
                var playerScoreLabel = Instantiate(_playerScoresPrefab, _scoreBoard.transform);
                var playerScoreDetails = playerScoreLabel.GetComponent<PlayerScores>();

                InitPlayerLabel(player, playerScoreDetails);
                UpdateScoreDisplay(player, playerScoreDetails);

                _playersScores.Add(playerScoreDetails);
            }
        }
    }

    private void InitPlayerLabel(Player player, PlayerScores playerScoreDetails)
    {
        playerScoreDetails.PlayerID = player.ActorNumber;
        playerScoreDetails.SetPlayerNameInLabel(player.NickName);
    }

    private void UpdateScoreDisplay(Player player, PlayerScores playerScoreDetails)
    {
        if (player.CustomProperties.ContainsKey("Kills") && player.CustomProperties.ContainsKey("Deaths"))
        {
            var kills = (int)player.CustomProperties["Kills"];
            var deaths = (int)player.CustomProperties["Deaths"];

            playerScoreDetails.UpdateScoresDisplay(kills, deaths);
        }
        else
        {
            Debug.LogWarning("Scores not set !");
        }
    }

    public void UpdateScores()
    {
        Debug.Log("Updating scoreboard...");
        foreach (var player in PhotonNetwork.PlayerList)
        {
            var playerScoreDetails = _playersScores.Where(x => x.PlayerID == player.ActorNumber).FirstOrDefault();
            UpdateScoreDisplay(player, playerScoreDetails);
            Debug.Log($"Player: {player.NickName} scores updated");
        }
    }
}