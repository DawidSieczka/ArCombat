using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    private ObjectPoolManager _objectPoolManager;

    private void Start()
    {
        _objectPoolManager = GetComponent<ObjectPoolManager>();
        _objectPoolManager.SpawnFromPool(NetworkObjectPoolTag.HealthPoint, transform.position, Quaternion.identity);
    }
}