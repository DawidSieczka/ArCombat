using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class QuickStartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    int multiplayerSceneIndex;

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }
    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room");
        StartGame();
    }
    void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Starting Game");
            PhotonNetwork.LoadLevel(multiplayerSceneIndex);
        }
    }
}

