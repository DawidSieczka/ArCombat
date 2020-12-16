using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviourPun
{

    bool isChange;
    const byte Change_Event = 0;
    void Start()
    {
        
    }

    void Update()
    {
        if (base.photonView.IsMine && Input.GetKeyDown(KeyCode.Space))
            MoveTheTestObject();
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

    private void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == Change_Event)
        {
            gameObject.transform.position = (Vector3)obj.CustomData;
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

        PhotonNetwork.RaiseEvent(Change_Event, this.gameObject.transform.position, RaiseEventOptions.Default, SendOptions.SendUnreliable);
    }
}
