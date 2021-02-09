using Assets._Scripts;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviourPun
{
    #region Singleton

    public static ObjectPoolManager Instance;

    private void Awake()
    {
        if (Instance != this && Instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion Singleton

    [Serializable]
    public class Pools
    {
        [Header("Enum NetworkObjectPoolTag")]
        public NetworkObjectPoolTag Tag;

        public GameObject Prefab;
        public int Size;
    }

    public List<Pools> PoolsCollection;
    private Dictionary<NetworkObjectPoolTag, Queue<GameObject>> _poolDictionary;

    private void Start()
    {
        _poolDictionary = new Dictionary<NetworkObjectPoolTag, Queue<GameObject>>();
        foreach (var pool in PoolsCollection)
        {
            Queue<GameObject> poolQueue = new Queue<GameObject>();

            for (int i = 0; i < pool.Size; i++)
            {
                var spawnedObject = MasterManager.NetworkInstantiate(pool.Prefab, transform.position, Quaternion.identity);

                poolQueue.Enqueue(spawnedObject);
                spawnedObject.GetComponent<GameObjectActivator>().Disactivate();
            }

            _poolDictionary.Add(pool.Tag, poolQueue);
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
}

public enum NetworkObjectPoolTag
{
    Bullet,
    HealthPoint,
    Ammo
}