using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attaches player into scripts that have to use player's gameobject
/// </summary>
public class PlayerManager : MonoBehaviourPun
{
    private void Awake()
    {
        if (base.photonView.IsMine)
        {
            FindObjectOfType<TouchScreen>().GetPlayerHorizontalMovement(this.gameObject);
        }
    }
}
