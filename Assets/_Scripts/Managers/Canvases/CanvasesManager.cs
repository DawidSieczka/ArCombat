using Photon.Pun;
using UnityEngine;

public class CanvasesManager : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject _createRoomButton;

    [SerializeField]
    private GameObject _playButton;

    [SerializeField]
    private GameObject _leaveButton;

    [SerializeField]
    private GameObject _canvasRoomLobby;

    [SerializeField]
    private GameObject _canvasRooms;

    private void Awake()
    {
        _createRoomButton.SetActive(false);
        _playButton.SetActive(false);
        _canvasRoomLobby.SetActive(false);
        _canvasRooms.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        _createRoomButton.SetActive(false);
        _playButton.SetActive(true);
        _leaveButton.SetActive(true);
        _canvasRoomLobby.SetActive(true);
    }

    public override void OnLeftRoom()
    {
        _createRoomButton.SetActive(true);
        _playButton.SetActive(false);
        _leaveButton.SetActive(false);
        _canvasRoomLobby.SetActive(false);
    }

    public override void OnJoinedLobby()
    {
        print("Joined to lobby");
        _createRoomButton.SetActive(true);
        _playButton.SetActive(false);
        _leaveButton.SetActive(false);
        _canvasRoomLobby.SetActive(false);
        _canvasRooms.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to master");
        print("Player nick: " + PhotonNetwork.LocalPlayer.NickName);
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }
}