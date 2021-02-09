using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool IsOccupied { get; set; }

    private PlayersSpawner _spawner;

    private void Awake()
    {
        _spawner = GetComponentInParent<PlayersSpawner>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.Enemy.ToString()))
        {
            Debug.Log($"Enemy detected - occupied: {other.gameObject.name}");
            IsOccupied = true;
            StartCoroutine(_spawner.ExcludeSpawnPointTemporarily(this));
        }
        else if (other.gameObject.CompareTag(Tag.Player.ToString()) && IsOccupied)
        {
            Debug.Log($"Moved object to another spawner: {other.gameObject.name}");
            _spawner.MoveObjectToSpawner(other.gameObject);
        }
    }
}