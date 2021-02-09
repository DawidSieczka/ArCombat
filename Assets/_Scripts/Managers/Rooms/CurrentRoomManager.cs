using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentRoomManager : MonoBehaviourPunCallbacks
{
    private int _sceneArenaId = 1;
    private int _sceneMenuId = 0;

    public void OnClick_StartPlaying()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(_sceneArenaId);
        }
    }

    public void OnClick_LeaveRoom()
    {
        try
        {
            PhotonNetwork.LeaveRoom();
        }
        catch (Exception ex)
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
                SceneManager.LoadScene(_sceneMenuId);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"3 Leaving room error catched: {ex.Message}");
        }

        base.OnLeftRoom();
    }
}