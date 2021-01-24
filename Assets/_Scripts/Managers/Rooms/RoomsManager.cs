using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RoomsManager : MonoBehaviourPunCallbacks
{
    public Transform _roomsContent;
    public Transform _playersContent;

    [SerializeField]
    private RoomListing _roomListingPrefab;

    [SerializeField]
    private PlayerListing _playerListingPrefab;

    private string _currentRoom;
    private List<RoomListing> _roomListings = new List<RoomListing>();
    private List<PlayerListing> _playerListings = new List<PlayerListing>();
    private RoomInfo newCreatedRoom;

    private string GetNewRoomName()
    {
        string roomName;
        do
        {
            roomName = $"room {Random.RandomRange(0, 1000)}";
        } while (_roomListings.Any(x=>x.RoomName == roomName));
        _currentRoom = roomName;
        return roomName;
    }



    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Entered room");
        PlayerListing listing = Instantiate(_playerListingPrefab, _playersContent);

        if (listing != null)
        {
            listing.SetPlayerInfo(newPlayer);
            _playerListings.Add(listing);
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _playerListings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_playerListings[index].gameObject);
            _playerListings.RemoveAt(index);
        }
        else
        {
            print($"player name to remove {otherPlayer.NickName}");
        }
    }

    public void OnClick_CreateRoom()
    {
        if (!PhotonNetwork.IsConnected)
            return;

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        PhotonNetwork.JoinOrCreateRoom(GetNewRoomName(), options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Room created");

        var listing = Instantiate(_roomListingPrefab, _roomsContent);
        if (listing != null)
        {
            listing.RoomName = _currentRoom;
            _roomListings.Add(listing);
            listing.GetComponent<Button>().interactable = false;
        }
        Debug.Log("Room added to list");
    }

    private void GetCurrentRoomPlayers()
    {
        foreach (var player in _playerListings)
        {
            Destroy(player.gameObject);
        }
        _playerListings.Clear();

        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }
    }

    private void AddPlayerListing(Player thisPlayer)
    {
        int index = _playerListings.FindIndex(x => x.Player == thisPlayer);
        if (index != -1)
        {
            _playerListings[index].SetPlayerInfo(thisPlayer);
        }
        else
        {
            PlayerListing listing = Instantiate(_playerListingPrefab, _playersContent);

            if (listing != null)
            {
                listing.SetPlayerInfo(thisPlayer);
                _playerListings.Add(listing);
            }
        }
       
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined to room");
        GetCurrentRoomPlayers();
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
                int index = _roomListings.FindIndex(x => x.RoomName == roomInfo.Name);
                if (index != -1)
                {
                    _roomListings[index].Destroy();
                    Destroy(_roomListings[index].gameObject);
                    _roomListings.RemoveAt(index);
                }
                else
                {
                    var roomsList = _roomsContent.GetComponentsInChildren<RoomListing>();
                    bool shouldRemove = true;
                    foreach (var room in roomsList)
                    {
                        if (room.RoomName == roomInfo.Name)
                        {
                            shouldRemove = false;
                            break;
                        }
                    }

                    if (shouldRemove)
                        roomList.Remove(roomInfo);
                }
            }
            else
            {
                int index = _roomListings.FindIndex(x => x.RoomName == roomInfo.Name);
                if (index == -1)
                {
                    AddNewCreatedRoomToList(roomInfo);
                }
                else
                {

                    print("dziwna sprawa !!!!!!!!!!!!!!!!!1");
                    //todo
                }
            }
        }
    }

    private void AddNewCreatedRoomToList(RoomInfo roomInfo)
    {
        var listing = Instantiate(_roomListingPrefab, _roomsContent);

        if (listing != null)
        {
            listing.RoomName = roomInfo.Name;
            listing.RoomCount = roomInfo.PlayerCount;
            _roomListings.Add(listing);
        }
    }
}