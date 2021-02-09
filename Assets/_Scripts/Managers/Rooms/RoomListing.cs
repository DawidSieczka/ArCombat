using Photon.Pun;
using System;
using UnityEngine;

public class RoomListing : MonoBehaviour
{
    private string _roomName;

    public string RoomName
    {
        get { return _roomName; }
        set
        {
            if (_text != null)
                _text.text = value;
            _roomName = value;
        }
    }

    public int RoomCount { get; set; }
    private TMPro.TextMeshProUGUI _text;

    private void Awake()
    {
        RoomCount = GetPlayersCount(_roomName);
        _text = GetComponentInChildren<TMPro.TextMeshProUGUI>();
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
        PhotonNetwork.JoinRoom(_roomName);
    }
}