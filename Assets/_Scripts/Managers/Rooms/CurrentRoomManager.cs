using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoomManager : MonoBehaviourPunCallbacks
{

    public void OnClick_StartPlaying()
    {
        //TODO Load map based on scene id - needed menu to choice map for current room
        int sceneArenaId = 1;

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(sceneArenaId);
        }
    }

    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
}
