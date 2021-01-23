using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject _playerScoresPrefab;

    [SerializeField]
    private GameObject _scoreBoard;

    private List<PlayerScores> _playersScores = new List<PlayerScores>();

    private MatchRules _matchRules;

    public void Init()
    {
        _matchRules = GetComponent<MatchRules>();
        if (PhotonNetwork.IsConnected)
        {
            foreach (var player in PhotonNetwork.PlayerList)
            {
                InstantiatePlayerLabel(player);
            }
        }
    }

    private void InstantiatePlayerLabel(Player player)
    {
        var playerScoreLabel = Instantiate(_playerScoresPrefab, _scoreBoard.transform);
        var playerScoreDetails = playerScoreLabel.GetComponent<PlayerScores>();

        SetupPlayerScores(player, playerScoreDetails);
    }

    public void SelectOwnerScores(int playerId)
    {
        var ownersScores = _playersScores.Where(x => x.PlayerID == playerId).FirstOrDefault();
        var scoresUIBackgrounds = ownersScores.gameObject.GetComponentsInChildren<Image>();

        foreach (var element in scoresUIBackgrounds)
        {
            element.color = new Color(0.614171f, 1, 0.2877358f);
        }
    }

    private void SetupPlayerScores(Player player, PlayerScores playerScoreDetails)
    {
        InitPlayerLabel(player, playerScoreDetails);
        UpdateScoreDisplay(player, playerScoreDetails);

        if (!_playersScores.Contains(playerScoreDetails))
            _playersScores.Add(playerScoreDetails);
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
            Debug.LogWarning("Scores not set! Init Kills and Deaths properties before setting them up");

            player.CustomProperties.Add("Kills", 0);
            player.CustomProperties.Add("Deaths", 0);

            SetupPlayerScores(player, playerScoreDetails);
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
            if (playerScoreDetails.Kills >= 10)
            {
                _matchRules.EnableWonScreen(player.NickName);
            }
        }
    }

    public void RemovePlayerLabel(Player player)
    {
        var scoresEntity = _playersScores.FirstOrDefault(x => x.PlayerID == player.ActorNumber);
        _playersScores.Remove(scoresEntity);
        var label = _scoreBoard.transform.GetComponentsInChildren<PlayerScores>().FirstOrDefault(x => x.PlayerID == player.ActorNumber);

        Destroy(label.gameObject);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        RemovePlayerLabel(otherPlayer);
        base.OnPlayerLeftRoom(otherPlayer);
    }

    public override void OnPlayerEnteredRoom(Player player)
    {
        InstantiatePlayerLabel(player);
    }
}