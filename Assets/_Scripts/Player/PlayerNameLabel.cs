using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerNameLabel : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TextMeshPro>().text = GetComponentInParent<PhotonView>().Controller.NickName;
        //transform.localScale = new Vector3()

    }
    private void Update()
    {
        
        transform.LookAt(Camera.main.transform);
    }
}
