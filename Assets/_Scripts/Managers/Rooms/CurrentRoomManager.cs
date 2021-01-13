using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentRoomManager : MonoBehaviourPunCallbacks
{
    int sceneArenaId = 1;
    int sceneMenuId = 0;
    public void OnClick_StartPlaying()
    {
        //TODO Load map based on scene id - needed menu to choice map for current room
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

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.CurrentRoom.IsVisible = true;
                PhotonNetwork.CurrentRoom.IsOpen = true;
                PhotonNetwork.LoadLevel(sceneMenuId);
            }
        }
    }

}
