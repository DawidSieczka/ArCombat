using Assets._Scripts;
using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviourPun
{
    #region Singleton

    public static ObjectPoolManager instance;

    private void Awake()
    {
        if (instance != this && instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    #endregion Singleton

    [Serializable]
    public class Pools
    {
        [Header("Enum NetworkObjectPoolTag")]
        public NetworkObjectPoolTag tag;

        public GameObject prefab;
        public int size;
    }

    public List<Pools> pools;
    private Dictionary<NetworkObjectPoolTag, Queue<GameObject>> _poolDictionary;

    private void Start()
    {
        _poolDictionary = new Dictionary<NetworkObjectPoolTag, Queue<GameObject>>();
        foreach (var pool in pools)
        {
            Queue<GameObject> poolQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                var spawnedObject = MasterManager.NetworkInstantiate(pool.prefab, transform.position, Quaternion.identity);

                spawnedObject.name = spawnedObject.GetComponent<PhotonView>().ViewID.ToString();
                poolQueue.Enqueue(spawnedObject);
                spawnedObject.GetComponent<GameObjectActivator>().Disactivate();
            }

            _poolDictionary.Add(pool.tag, poolQueue);
        }
    }

    public GameObject SpawnFromPool(NetworkObjectPoolTag tag, Vector3 position, Quaternion rotation)
    {
        if (!_poolDictionary.ContainsKey(tag))
        {
            Debug.LogError($"Pool dictionary doesn't contain key: {tag.ToString()}");
            return null;
        }

        var objToSpawn = _poolDictionary[tag].Dequeue();

        objToSpawn.transform.position = position;
        objToSpawn.transform.rotation = rotation;

        var pooledObj = objToSpawn.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        _poolDictionary[tag].Enqueue(objToSpawn);

        return objToSpawn;
    }
    public GameObject SpawnFromPool(NetworkObjectPoolTag tag, Vector3 position, Quaternion rotation, ShootingMetadata shootingMetadata)
    {
        var obj = SpawnFromPool(tag, position, rotation);
        obj.GetComponent<BulletBehaviour>().InvokeShoot(shootingMetadata);
        return obj;
    }
    private List<PhotonView> _photonViews;

    private void InitListOfPhotonViewObjectsIfNotExists()
    {
        if (_photonViews == null)
        {
            _photonViews = FindObjectsOfType<PhotonView>().ToList();
        }
    }

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

            print($"uuuuu: {photonViewID} is found");
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
}

public enum NetworkObjectPoolTag
{
    Bullet
}