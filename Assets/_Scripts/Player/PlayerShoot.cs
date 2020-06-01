using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    PlayerAim _playerAim;
    Vector3? _aimedPoint;
    ButtonEvent _shootButton;
    public GameObject bullet;
    public TextMeshProUGUI ammoText;
    int _gunMagazine = 64;
    int _oneMagazineSet = 32; //this is amount of shootable ammount of the gun
    int _ammoAmount = 32;
    bool isReadToShoot = true;

    void Start() {
        _playerAim = GetComponent<PlayerAim>();
        _shootButton = FindObjectOfType<ButtonEvent>();
        _shootButton.OnShoot.AddListener(Shoot);
    }

    void Update() {
        _aimedPoint = _playerAim.aimedTargetPosition;
    }

    void SetAmmo()
    {
        ammoText.text = $"{_ammoAmount}/{_gunMagazine}";
    }

    void SetSpawnSide(){
        //TODO
    }

    void Shoot(){
        if (_aimedPoint != null && isReadToShoot && _ammoAmount > 0){
            _ammoAmount--;
            if (_ammoAmount < 1)
            {
                isReadToShoot = false;
                Reload();
            }
            var bulletInstance = Instantiate(bullet,transform.position, Quaternion.identity);
            bulletInstance.GetComponent<BulletBehaviour>().InvokeShoot(_aimedPoint.Value);
            SetAmmo();
        }
    }
    void Reload()
    {
        StartCoroutine(ReloadCoolDown());
        //TODO add reload button and replace N amount with full magazine and add N amount to _gunMagazine.
        if (_gunMagazine >= _oneMagazineSet)
        {
            _ammoAmount = 32;
            _gunMagazine -= 32;
        }
    }
    IEnumerator ReloadCoolDown()
    {
        yield return new WaitForSeconds(2f);
        isReadToShoot = true;
    }
}
