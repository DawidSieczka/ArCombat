using Photon.Pun;
using System;
using System.Linq;
using UnityEngine;

public class Room : MonoBehaviour
{
    public string RoomName { get; set; }
    public int RoomCount { get; set; }

    private void Awake()
    {
        RoomCount = GetPlayersCount(RoomName);
    }

    public int GetPlayersCount(string roomName)
    {
        int playersCount = 0;
        try
        {
            var isPhotonNetworkReadyToWork = (!PhotonNetwork.IsConnected ||
                                           PhotonNetwork.CurrentRoom == null ||
                                           PhotonNetwork.CurrentRoom.Players == null);
            if (isPhotonNetworkReadyToWork)
            {
                throw new Exception("Photon Network is not ready to work...");
            }
            //TODO
            //Get count of this room !
        }
        catch (Exception ex)
        {
            Debug.LogException(ex, this);
        }

        return playersCount;
    }

    public void OnClick_JoinRoom()
    {
        PhotonNetwork.JoinRoom(RoomName);
    }
}