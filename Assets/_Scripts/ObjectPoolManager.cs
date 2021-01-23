using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
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
            //Queue<GameObject> poolQueue = new Queue<GameObject>();
            var networkPoolQueue = new PhotonStreamQueue(pool.size);
            for (int i = 0; i < pool.size; i++)
            {
                var spawnedObject = MasterManager.NetworkInstantiate(pool.prefab, transform.position, Quaternion.identity);
                //networkPoolQueue.
                //poolQueue.Enqueue(spawnedObject);
                spawnedObject.SetActive(false);
            }
            
            //_poolDictionary.Add(pool.tag, poolQueue);
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

        objToSpawn.SetActive(true);
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
}

public enum NetworkObjectPoolTag
{
    Bullet
}