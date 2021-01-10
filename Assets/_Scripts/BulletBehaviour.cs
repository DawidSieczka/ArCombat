using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class BulletBehaviour : MonoBehaviourPun
{
    private bool isInvoked = false;
    private Vector3 _direction;
    private float _speed = 2f;
    private const byte TAKEDAMAGE_EVENTCODE = 6; //todo zrobic wspolna klase dla event code'ów
    private const byte test_EVENTCODE = 7;
    public int BulletDamage = 10;
    private Player _bulletOwner;

    private void Update()
    {
        if (isInvoked)
        {
            transform.Translate(_direction * Time.deltaTime * _speed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (base.photonView.IsMine)
        {
            if (other.gameObject.CompareTag(Tag.Player.ToString()))
            {
                Debug.Log("The bullet hit the player - no action");
            }
            else if (other.gameObject.CompareTag(Tag.Enemy.ToString()))
            {
                Debug.Log($"(Damage) object that has been hit: {other.gameObject.name}, {other.gameObject.tag} | Hit with value: {BulletDamage}");
                var hittenPlayer = other.gameObject.GetComponent<PhotonView>();
                //PhotonNetwork.RaiseEvent(TAKEDAMAGE_EVENTCODE, BulletDamage, RaiseEventOptions.Default, SendOptions.SendReliable);
                //PhotonNetwork.RaiseEvent(test_EVENTCODE, _bulletOwner, RaiseEventOptions.Default, SendOptions.SendReliable);
                hittenPlayer.RPC("GetHit", hittenPlayer.Controller, 10, _bulletOwner);
                PhotonNetwork.Destroy(this.gameObject.GetComponent<PhotonView>());
            }
            else if (other.gameObject.CompareTag(Tag.Player.ToString()) == false)
            {
                Debug.Log($"(Destroy) object that has been hit: {other.gameObject.name}, {other.gameObject.tag}");
                PhotonNetwork.Destroy(this.gameObject.GetComponent<PhotonView>());
            }
        }
    }

    public void InvokeShoot(Vector3 direction, Player bulletOwner)
    {
        _bulletOwner = bulletOwner;
        this._direction = direction - transform.position;
        isInvoked = true;
        Destroy(this.gameObject, 1.5f);
    }
}