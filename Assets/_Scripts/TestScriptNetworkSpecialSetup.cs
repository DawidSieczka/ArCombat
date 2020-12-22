using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class TestScriptNetworkSpecialSetup : MonoBehaviourPun, IPunObservable
{
    private bool isChange;
    private const byte Change_Event = 0;

    private void Start()
    {
    }

    private void Update()
    {
        //if (base.photonView.IsMine && Input.GetKeyDown(KeyCode.Space))
        //    MoveTheTestObject();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else if (stream.IsWriting)
        {
            transform.position = (Vector3)stream.ReceiveNext();
            transform.rotation = (Quaternion)stream.ReceiveNext();
        }
    }
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    protected virtual void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == Change_Event)
        {
            //gameObject.transform.position = (Vector3)obj.CustomData;
        }
    }

    private void MoveTheTestObject()
    {
        Vector3 dir;
        if (isChange)
            dir = Vector3.up;
        else
            dir = Vector3.down;

        this.gameObject.transform.position += dir;

        isChange = !isChange;

        RaiseEvent(Change_Event, this.gameObject.transform.position);
    }

    protected void RaiseEvent(byte EVENT_CODE, object eventContent)
    {
        PhotonNetwork.RaiseEvent(EVENT_CODE, eventContent, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    }
}