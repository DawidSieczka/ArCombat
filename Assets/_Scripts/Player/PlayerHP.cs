using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;
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

            if (_hp > 0)
            {
                _hp -= damage;
                var percentageOfDamage = ((float)damage / 100);
                _hpBar.transform.localScale -= new Vector3(percentageOfDamage, 0, 0);
            }
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
        this.gameObject.SetActive(false);
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
    private void UpdateScoreboard()
    {
        var players = PhotonNetwork.PlayerList;
        foreach (var player in players)
        {
            Debug.Log($"Player {player.NickName} has {(int)player.CustomProperties["Kills"]} points and {(int)player.CustomProperties["Deaths"]} deaths!");
        }

        Debug.Log($"Some player got 1 point !!!");
    }

    [PunRPC]
    public void GetHit(int damage, Player enemy)
    {
        if (photonView.IsMine)
        {
            Debug.Log($"The player {photonView.ControllerActorNr} got hit with dmg: {damage} from player: {enemy.ActorNumber}");
            SubtractHP(damage);
            if (_hp <= 0)
            {
                Debug.Log($"{enemy.NickName} killed {photonView.Controller.NickName}");
                //enemy.SetScore(enemy.GetScore() + 1);

                var enemyKills = (int)enemy.CustomProperties["Kills"];
                enemy.CustomProperties["Kills"] = ++enemyKills;
                enemy.SetCustomProperties(enemy.CustomProperties);

                var currentPlayerDeaths = (int)photonView.Controller.CustomProperties["Deaths"];
                photonView.Controller.CustomProperties["Deaths"] = ++currentPlayerDeaths;
                photonView.Controller.SetCustomProperties(photonView.Controller.CustomProperties);
                
                photonView.RPC("OnScoresUpdate", RpcTarget.All);
                SetMinHP();
            }
        }
    }
}