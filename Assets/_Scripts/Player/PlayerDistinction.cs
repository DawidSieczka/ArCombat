using Photon.Pun;
using UnityEngine;

public class PlayerDistinction : MonoBehaviourPun
{
    private void Start()
    {
        var playerColor = Color.green;
        var enemyColor = Color.red;
     
        if (base.photonView.IsMine)
        {
            transform.tag = Tag.Player.ToString();
            GetComponentInChildren<Light>().color = playerColor;
        }
        else
        {
            transform.tag = Tag.Enemy.ToString();
            GetComponentInChildren<Light>().color = enemyColor;
        }
    }
}