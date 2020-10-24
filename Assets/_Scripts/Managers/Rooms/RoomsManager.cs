using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class RoomsManager : MonoBehaviourPunCallbacks
{
    public Transform _content;

    [SerializeField]
    private Room _roomPrefab;
    static int num = 1;
    private string _newRoomName = $"(Eu) room {++num}";
    private List<Room> _listings = new List<Room>();

    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(_newRoomName, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created");
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Room creating failed");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        print("Room list update");
        foreach (var roomInfo in roomList)
        {
            if (roomInfo.RemovedFromList)
            {
                int index = _listings.FindIndex(x => x.RoomName == roomInfo.Name);
                if (index != -1)
                {
                    Destroy(_listings[index].gameObject);
                    _listings.RemoveAt(index);
                }
            }
            else
            {
                int index = _listings.FindIndex(x => x.RoomName == roomInfo.Name);
                if (index == -1)
                {
                    var listing = Instantiate(_roomPrefab, _content);

                    if (listing != null)
                    {
                        listing.RoomName = roomInfo.Name;
                        listing.RoomCount = roomInfo.PlayerCount;
                        _listings.Add(listing);
                    }
                }
                else
                {
                    //todo
                }
            }
        }
    }
}