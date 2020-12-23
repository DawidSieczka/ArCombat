using Photon.Pun;
using UnityEngine;

public class PlayerDistinction : MonoBehaviourPun
{
    private void Start()
    {
        var playerColor = Color.green;
        var enemyColor = Color.red;
     
        if (base.photonView.IsMine)
            GetComponentInChildren<Light>().color = playerColor;
        else
            GetComponentInChildren<Light>().color = enemyColor;
    }
}