using Photon.Pun;
using UnityEngine;

public class PlayerScoresManager : MonoBehaviourPun
{
    private ScoreBoard _scoreBoard;

    private void Awake()
    {
        photonView.Controller.CustomProperties.Add("Kills", 0);
        photonView.Controller.CustomProperties.Add("Deaths", 0);

        _scoreBoard = GameObject.FindGameObjectWithTag("UI").GetComponent<ScoreBoard>();
    }

    [PunRPC]
    public void OnScoresUpdate()
    {
        if (photonView.IsMine)
        {
            _scoreBoard.UpdateScores();
        }
    }

}