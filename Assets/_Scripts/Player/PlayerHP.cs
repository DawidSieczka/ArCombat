using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerHP : MonoBehaviourPun
{
    private int _hp = 100;

    private GameObject _hpBar;

    private void Start()
    {
        _hpBar = GameObject.FindGameObjectWithTag(Tag.PlayerHp.ToString());
        SetMaxHP();
    }

    public void SubtractHP(int damage)
    {
        if (base.photonView.IsMine)
        {
            Debug.Log($"Player: {gameObject.name} | {this.gameObject.tag}, get hit: {damage}");

            _hp -= damage;
            var percentageOfDamage = ((float)damage / 100);
            _hpBar.transform.localScale -= new Vector3(percentageOfDamage, 0, 0);
        }
    }

    public void IncreaseHP(int health)
    {
        if (base.photonView.IsMine)
        {
            if (_hp < 100)
            {
                _hp += health;
                var percentageOfHealth = ((float)health / 100);
                _hpBar.transform.localScale += new Vector3(percentageOfHealth, 0, 0);
            }
            if (_hp >= 100)
            {
                SetMaxHP();
            }
        }
    }

    public void SetMaxHP()
    {
        _hp = 100;
        _hpBar.transform.localScale = new Vector3(1, _hpBar.transform.localScale.y, _hpBar.transform.localScale.z);
    }

    public void SetMinHP()
    {
        _hp = 0;
        _hpBar.transform.localScale = new Vector3(0, _hpBar.transform.localScale.y, _hpBar.transform.localScale.z);
        //photonView.RPC("HidePlayerModel", RpcTarget.All, photonView.Controller.ActorNumber);
        StartCoroutine(SetAsDead());
    }

    [PunRPC]
    public void HidePlayerModel(int id)
    {
        FindObjectsOfType<PlayerHP>().Where(x => x.photonView.Controller.ActorNumber == id).FirstOrDefault().SetAsDead();
    }

    private void Update()
    {
        //Debug
        if (photonView.IsMine)
        {
            DebugActions();
        }
    }

    private void DebugActions()
    {
        if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.X))
        {
            SubtractHP(10);
        }
        if (Debug.isDebugBuild && Input.GetKeyDown(KeyCode.C))
        {
            print(photonView.Controller.GetScore());
            IncreaseHP(10);
        }
    }

    [PunRPC]
    public void GetHit(int damage, Player enemy)
    {
        if (photonView.IsMine)
        {
            Debug.Log($"The player {photonView.ControllerActorNr} got hit with dmg: {damage} from player: {enemy.ActorNumber}");
            if (_hp > 0)
            {
                SubtractHP(damage);
            }
            else
            {
                Debug.LogWarning("Player is not alive but got hit...");
                return;
            }

            if (_hp <= 0)
            {
                Debug.Log($"{enemy.NickName} killed {photonView.Controller.NickName}");
                
                SetPointsForDeath();
                photonView.RPC("OnScoresUpdate", RpcTarget.All);
                SetMinHP();
            }

            void SetPointsForDeath()
            {
                var enemyKills = (int)enemy.CustomProperties["Kills"];
                enemy.CustomProperties["Kills"] = ++enemyKills;
                enemy.SetCustomProperties(enemy.CustomProperties);

                var currentPlayerDeaths = (int)photonView.Controller.CustomProperties["Deaths"];
                photonView.Controller.CustomProperties["Deaths"] = ++currentPlayerDeaths;
                photonView.Controller.SetCustomProperties(photonView.Controller.CustomProperties);
            }
        }
    }

    private IEnumerator SetAsDead()
    {
        transform.position = new Vector3(8.7f, -20.4f, 7.45f);
        var rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        yield return new WaitForSeconds(2);
        rb.useGravity = true;
        FindObjectOfType<Spawner>().MoveObjectToSpawner(gameObject);
        SetMaxHP();

    }
}