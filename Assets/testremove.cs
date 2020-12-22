using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class testremove : MonoBehaviourPun, IPunObservable
{
    private const byte Change_Event = 3;
    bool isChange;
    TextMeshProUGUI text;
    void Start()
    {
        text = GameObject.FindGameObjectWithTag("asdasd").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (base.photonView.IsMine)
            {
                text.text = base.photonView.Owner.NickName;
                Debug.Log(base.photonView.Owner.NickName);
                MoveTheTestObject();
            }
        }
    }
    protected void RaiseEvent(byte EVENT_CODE, object eventContent)
    {
        PhotonNetwork.RaiseEvent(EVENT_CODE, eventContent, RaiseEventOptions.Default, SendOptions.SendUnreliable);
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
            transform.position = (Vector3)obj.CustomData;
        }
    }

    private void MoveTheTestObject()
    {
            Vector3 dir;
            if (isChange)
                dir = Vector3.up;
            else
                dir = Vector3.down;

            transform.position += dir;

            isChange = !isChange;

            //RaiseEvent(Change_Event, transform.position);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if (stream.IsWriting)
        //{
        //    // We own this player: send the others our data
        //    stream.SendNext(transform.position);
        //}
        //else if(stream.IsReading)
        //{
        //    // Network player, receive data
        //    transform.position = (Vector3)stream.ReceiveNext();
        //}
    }
}
