using Photon.Pun;
using TMPro;
using UnityEngine;

public class PlayerNameLabel : MonoBehaviour
{
    private void Start()
    {
        GetComponent<TextMeshPro>().text = GetComponentInParent<PhotonView>().Controller.NickName;
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform);
    }
}