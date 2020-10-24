using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class NetworkingTestJoin : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject _canvasRooms;

    private void Awake()
    {
        _canvasRooms.SetActive(false);
    }

    private void Start()
    {
        print("Connecting to master...");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        print("Connected to master");
        print("Player nick: " + PhotonNetwork.LocalPlayer.NickName);
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        print("Joined to lobby");
        _canvasRooms.SetActive(true);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server for reason: " + cause.ToString());
        base.OnDisconnected(cause);
    }
}