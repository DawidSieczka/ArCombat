using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomManager : MonoBehaviourPunCallbacks
{
    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
