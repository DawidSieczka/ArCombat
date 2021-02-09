using Photon.Pun;
using UnityEngine;

public class PlayerScoresManager : MonoBehaviourPun
{
    private ScoreBoard _scoreBoard;

    private void Awake()
    {
        _scoreBoard = GameObject.FindGameObjectWithTag("UI").GetComponent<ScoreBoard>();

        if (photonView.IsMine)
        {
            _scoreBoard.Init();
            _scoreBoard.SelectOwnerScores(photonView.Controller.ActorNumber);
        }
    }

    [PunRPC]
    public void OnScoresUpdate()
    {
        Debug.Log($"{photonView.Controller.NickName} updates scoreboard");
        _scoreBoard.UpdateScores();
    }
}