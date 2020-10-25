using Photon.Pun;
using Photon.Realtime;

public class NetworkingTestJoin : MonoBehaviourPunCallbacks
{
    private void Start()
    {
        print("Connecting to master...");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.NickName = MasterManager.GameSettings.NickName;
        PhotonNetwork.GameVersion = MasterManager.GameSettings.GameVersion;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        print("Disconnected from server for reason: " + cause.ToString());
        base.OnDisconnected(cause);
    }
}