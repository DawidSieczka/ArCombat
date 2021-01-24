using Assets._Scripts;
using Photon.Pun;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BulletsManager : MonoBehaviourPun
{
    private List<PhotonView> _photonViews;


    [PunRPC]
    private void EnableBulletInEnemyView(int photonViewID)
    {
        InitListOfPhotonViewObjectsIfNotExists();

        var bullet = _photonViews.First(x => x.ViewID == photonViewID);
        if (bullet == null)
            Debug.LogError($"{photonViewID} is not found");
        else
        {
            var bulletActivator = bullet.gameObject.GetComponent<GameObjectActivator>();
            bulletActivator.Activate();
        }
    }

    [PunRPC]
    private void DisableBulletInEnemyView(int photonViewID)
    {
        InitListOfPhotonViewObjectsIfNotExists();

        var bullet = _photonViews.First(x => x.ViewID == photonViewID);

        if (bullet == null)
            Debug.LogError("Something went wrong - can not disable bullet");
        else
        {
            var bulletActivator = bullet.gameObject.GetComponent<GameObjectActivator>();
            bulletActivator.Disactivate();
        }
    }

    private void InitListOfPhotonViewObjectsIfNotExists()
    {
        if (_photonViews == null)
        {
            _photonViews = FindObjectsOfType<PhotonView>().ToList();
        }
    }
}