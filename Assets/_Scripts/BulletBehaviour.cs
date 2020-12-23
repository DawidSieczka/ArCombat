using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class BulletBehaviour : MonoBehaviourPun
{

    bool isInvoked = false;
    Vector3 _direction;
    float _speed = 2f;

    void Start(){

    }

    void Update(){
        if (isInvoked)
        {
            transform.Translate(_direction * Time.deltaTime * _speed);
            Destroy(this.gameObject,1.5f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag(Tag.Player.ToString()))
            PhotonNetwork.Destroy(this.gameObject.GetComponent<PhotonView>());
    }

    public void InvokeShoot(Vector3 direction)
    {
        this._direction = direction - transform.position;
        isInvoked = true;
    }
}
