using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayersSpawner : MonoBehaviourPunCallbacks
{
    private List<SpawnPoint> _spawnPoints;

    [SerializeField]
    private GameObject _player;

    private const byte SPAWNS_EVENT = 5;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<SpawnPoint>().ToList();
        if (PhotonNetwork.IsMasterClient)
        {
            foreach (var player in PhotonNetwork.PlayerList)
            {
                SpawnPlayerInDrawedSpawnPoint(player);
            }
        }
    }

    private void SpawnPlayerInDrawedSpawnPoint(Player player)
    {
        Debug.Log($"player thats going to spawn {player.NickName}");
        var anySpawn = TakeRandomSpawnPoint(_spawnPoints.Count());
        RaiseEventOptions rso = new RaiseEventOptions { TargetActors = new int[] { player.ActorNumber } };
        PhotonNetwork.RaiseEvent(SPAWNS_EVENT, _spawnPoints[anySpawn].transform.position, rso, SendOptions.SendReliable);
        StartCoroutine(ExcludeSpawnPointTemporarily(_spawnPoints[anySpawn]));
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnSpawn_EventReceived;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnSpawn_EventReceived;
    }

    private void OnSpawn_EventReceived(EventData data)
    {
        if (data.Code == SPAWNS_EVENT)
        {
            InvokeSpawning((Vector3)data.CustomData);
        }
    }

    public void InvokeSpawning(Vector3 spawnPointCoordinates)
    {
        MasterManager.NetworkInstantiate(_player, spawnPointCoordinates, _player.transform.rotation);
    }

    public void InvokeSpawning()
    {
        var anySpawn = TakeRandomSpawnPoint(_spawnPoints.Count());

        MasterManager.NetworkInstantiate(_player, _spawnPoints[anySpawn].transform.position, _player.transform.rotation);

        StartCoroutine(ExcludeSpawnPointTemporarily(_spawnPoints[anySpawn]));
    }

    public IEnumerator ExcludeSpawnPointTemporarily(SpawnPoint spawnPoint)
    {
        ExcludeSpawnPoint(spawnPoint);
        yield return new WaitForSeconds(5);
        IncludeSpawnPoint(spawnPoint);
    }

    private void ExcludeSpawnPoint(SpawnPoint spawnPoint)
    {
        Debug.Log($"Excluded spawn point: {spawnPoint.gameObject.name}");
        spawnPoint.gameObject.SetActive(false);
        _spawnPoints.Remove(spawnPoint);
    }

    private void IncludeSpawnPoint(SpawnPoint spawnPoint)
    {
        Debug.Log($"Included spawn point: {spawnPoint.gameObject.name}");
        spawnPoint.gameObject.SetActive(true);
        _spawnPoints.Add(spawnPoint);
    }

    public void MoveObjectToSpawner(GameObject gameObject)
    {
        if (gameObject.GetComponent<PhotonView>().IsMine)
        {
            var anySpawn = TakeRandomSpawnPoint(_spawnPoints.Count());
            gameObject.transform.position = _spawnPoints[anySpawn].transform.position;
        }
    }

    private int TakeRandomSpawnPoint(int amount)
    {
        System.Random rand = new System.Random();
        int anySpawn = 0;
        do
        {
            Debug.Log($"Spawn: {anySpawn}, isOccupied: {_spawnPoints[anySpawn].IsOccupied}");

            anySpawn = rand.Next(amount-1);
        } while (_spawnPoints[anySpawn].IsOccupied);
        return anySpawn;
    }
}