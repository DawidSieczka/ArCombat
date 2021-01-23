using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class BulletBehaviour : MonoBehaviourPun, IPooledObject, IPunPrefabPool
{
    private bool isInvoked = false;
    private Vector3 _direction;
    private float _speed = 2f;
    private const byte TAKEDAMAGE_EVENTCODE = 6; //todo zrobic wspolna klase dla event code'ów
    private const byte test_EVENTCODE = 7;
    public int BulletDamage = 10;
    private Player _bulletOwner;

    private void Awake()
    {
        if (!photonView.IsMine)
        {

            gameObject.SetActive(false);
        }
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
                gameObject.SetActive(false);
                //PhotonNetwork.Destroy(this.gameObject.GetComponent<PhotonView>());

            }
            else if (other.gameObject.CompareTag(Tag.Player.ToString()) == false)
            {
                Debug.Log($"(Destroy) object that has been hit: {other.gameObject.name}, {other.gameObject.tag}");
                gameObject.SetActive(false);
                //PhotonNetwork.Destroy(this.gameObject.GetComponent<PhotonView>());
            }
        }
        
        if (other.gameObject.CompareTag(Tag.Enemy.ToString()))
        {
            gameObject.SetActive(false);
        }

    }

    public void InvokeShoot(Vector3 direction, Player bulletOwner)
    {
        _bulletOwner = bulletOwner;
        this._direction = direction - transform.position;
        isInvoked = true;
        StartCoroutine(HideBullet());
        //Destroy(this.gameObject, 1.5f);
    }
    IEnumerator HideBullet()
    {
        yield return new WaitForSeconds(1.5f);
        gameObject.SetActive(false);
    }
    public void OnObjectSpawn()
    {
        if (!photonView.IsMine)
        {
            gameObject.SetActive(true);
        }
        //Wykonuje sie na starcie utworzenia obiektu
    }
    //private void Start()
    //{
    //    var a =Instantiate(0, transform.position, Quaternion.identity);
    //}
    public GameObject Instantiate(string prefabId, Vector3 position, Quaternion rotation)
    {
        Debug.Log("asd");
        return new GameObject();
    }

    public void Destroy(GameObject gameObject)
    {
        print(gameObject.name);
    }
}