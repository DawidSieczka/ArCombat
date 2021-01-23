using Photon.Pun;
using System;
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
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(sceneArenaId);
        }
    }

    public void OnClick_LeaveRoom()
    {
        try
        {
            //PhotonNetwork.CurrentRoom.IsVisible = true;
            //PhotonNetwork.CurrentRoom.IsOpen = true;
            PhotonNetwork.LeaveRoom();
        }
        catch(Exception ex)
        {
            Debug.LogError($"2 Leaving room error catched: {ex.Message}");
        }
    }

    public override void OnLeftRoom()
    {
        try
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                SceneManager.LoadScene(sceneMenuId);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"3 Leaving room error catched: {ex.Message}");
        }

        base.OnLeftRoom();
    }

}
