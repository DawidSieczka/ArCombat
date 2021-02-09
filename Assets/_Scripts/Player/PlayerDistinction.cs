using Photon.Pun;
using TMPro;
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
            GetComponentInChildren<TextMeshPro>().color = playerColor;
        }
        else
        {
            transform.tag = Tag.Enemy.ToString();
            GetComponentInChildren<TextMeshPro>().color = enemyColor;
        }
    }
}