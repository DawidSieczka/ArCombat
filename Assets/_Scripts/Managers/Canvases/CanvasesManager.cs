using Photon.Pun;
using Photon.Realtime;
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

    [SerializeField]
    private GameObject _canvasInputName;

    private void Awake()
    {
        _createRoomButton.SetActive(false);
        _playButton.SetActive(false);
        _canvasRoomLobby.SetActive(false);
        _canvasRooms.SetActive(false);
        _canvasInputName.SetActive(true);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        Debug.Log("Player nick: " + PhotonNetwork.LocalPlayer.NickName);
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }

    public void JoinServer(string playerName)
    {
        if (playerName.Length < 3)
        {
            Debug.LogError("Name is too short");
            return;
        }
        Debug.Log("Connecting to master...");
        _canvasInputName.SetActive(false);
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 10;
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = playerName;
        PhotonNetwork.GameVersion = "0.0.0";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined to lobby");
        _createRoomButton.SetActive(true);
        _playButton.SetActive(false);
        _leaveButton.SetActive(false);
        _canvasRoomLobby.SetActive(false);
        _canvasRooms.SetActive(true);
        _canvasInputName.SetActive(false);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        _createRoomButton.SetActive(false);
        _canvasInputName.SetActive(false);
        _playButton.SetActive(true);
        _leaveButton.SetActive(true);
        _canvasRoomLobby.SetActive(true);
    }

    public override void OnLeftRoom()
    {
        Debug.Log("Left room");
        _createRoomButton.SetActive(true);
        _playButton.SetActive(false);
        _leaveButton.SetActive(false);
        _canvasRoomLobby.SetActive(false);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("Disconnected from server for reason: " + cause.ToString());
        base.OnDisconnected(cause);
        _canvasInputName.SetActive(false);
    }
}