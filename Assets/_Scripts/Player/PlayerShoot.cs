using Assets._Scripts;
using Photon.Pun;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerShoot : MonoBehaviourPun
{
    private PlayerAim _playerAim;
    private Vector3? _aimedPoint;
    private ButtonEvent _shootButton;
    public GameObject bullet;
    private TextMeshProUGUI ammoText;
    private int _gunMagazine = 128;
    private int _oneMagazineSet = 32; //this is amount of shootable ammount of the gun
    private int _ammoAmount = 32;
    private bool isReadToShoot = true;
    private ObjectPoolManager _objectPoolManager;
    private ShootingMetadata _shootingMetadata;
    private void Start()
    {
        _playerAim = GetComponent<PlayerAim>();
        _shootButton = FindObjectOfType<ButtonEvent>();
        _shootButton.OnShoot.AddListener(Shoot);
        ammoText = GameObject.FindGameObjectWithTag("AmmoInfo").GetComponent<TextMeshProUGUI>();
        _objectPoolManager = FindObjectOfType<ObjectPoolManager>();
        _shootingMetadata = new ShootingMetadata();
    }

    private void Update()
    {
        if (base.photonView.IsMine)
            _aimedPoint = _playerAim.aimedTargetPosition;
    }

    private void UpdateAmmoText()
    {
        ammoText.text = $"{_ammoAmount}/{_gunMagazine}";
    }

    private void SetSpawnSide()
    {
        //TODO
    }

    private void Shoot()
    {
        if (base.photonView.IsMine)
        {
            if (_aimedPoint != null && isReadToShoot && _ammoAmount > 0)
            {
                _ammoAmount--;
                if (_ammoAmount < 1)
                {
                    isReadToShoot = false;
                    Reload();
                }
                var player = photonView.Controller;
                _shootingMetadata.Player = player;
                _shootingMetadata.Direction = _aimedPoint.Value;
                _objectPoolManager.SpawnFromPool(NetworkObjectPoolTag.Bullet, transform.position, Quaternion.identity,_shootingMetadata);
                UpdateAmmoText();
            }
        }
    }

    private void Reload()
    {
        StartCoroutine(ReloadCoolDown());
        //TODO add reload button and replace N amount with full magazine and add N amount to _gunMagazine.
        if (_gunMagazine >= _oneMagazineSet)
        {
            _ammoAmount = 32;
            _gunMagazine -= 32;
        }
    }

    private IEnumerator ReloadCoolDown()
    {
        yield return new WaitForSeconds(2f);
        isReadToShoot = true;
    }
}