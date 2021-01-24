using Assets._Scripts;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class BulletBehaviour : MonoBehaviourPun, IPooledObject
{
    private bool isInvoked = false;
    private Vector3 _direction;
    private float _speed = 2f;
    public int BulletDamage = 10;
    private Player _bulletOwner;
    private GameObjectActivator _activator;
    private ObjectPoolManager _objectPoolManager;
    private void Awake()
    {
        _objectPoolManager = FindObjectOfType<ObjectPoolManager>();
        _activator = GetComponent<GameObjectActivator>();
        _activator.Disactivate();
    }

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
                hittenPlayer.RPC("GetHit", hittenPlayer.Controller, 10, _bulletOwner);
                OnObjectDestroy();
            }
            else if (other.gameObject.CompareTag(Tag.Player.ToString()) == false)
            {
                Debug.Log($"(Destroy) object that has been hit: {other.gameObject.name}, {other.gameObject.tag}");
                OnObjectDestroy();
            }
        }
    }

    public void InvokeShoot(ShootingMetadata metaData)
    {
        _bulletOwner = metaData.Player;
        this._direction = metaData.Direction - transform.position;
        isInvoked = true;
        StartCoroutine(HideBullet());
    }

    private IEnumerator HideBullet()
    {
        yield return new WaitForSeconds(4);
        OnObjectDestroy();
    }

    private void OnEnable()
    {
        //StartCoroutine(check());
    }

    public void OnObjectSpawn()
    {
        var id = photonView.ViewID;
        _objectPoolManager.photonView.RPC("EnableBulletInEnemyView", RpcTarget.All, id);

        //locally
        _activator.Activate();
    }

    public void OnObjectDestroy()
    {
        var id = photonView.ViewID;
        _objectPoolManager.photonView.RPC("DisableBulletInEnemyView", RpcTarget.All, id);

        //locally
        _activator.Disactivate();
    }
}