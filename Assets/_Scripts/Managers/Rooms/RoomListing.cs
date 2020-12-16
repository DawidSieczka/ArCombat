using Photon.Pun;
using Photon.Realtime;
using System;
using System.Linq;
using UnityEngine;

public class RoomListing : MonoBehaviour
{
    public string RoomName { get; set; }

    public int RoomCount { get; set; }
    private TMPro.TextMeshProUGUI text;
    private void Awake()
    {
        RoomCount = GetPlayersCount(RoomName);
        text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
    }

    public void SetRoomName(string name)
    {
        RoomName = name;
        text.text = name;
    }

    public void Destroy()
    {
        Destroy(gameObject);
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
            Debug.LogError(ex.Message);
        }

        return playersCount;
    }

    public void OnClick_JoinRoom()
    {
        PhotonNetwork.JoinRoom(RoomName);
    }
}