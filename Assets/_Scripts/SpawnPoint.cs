using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool isOccupied { get; set; }

    private Spawner _spawner;

    private void Awake()
    {
        _spawner = GetComponentInParent<Spawner>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.Enemy.ToString()))
        {
            print($"Enemy detected - occupied: {other.gameObject.name}");
            isOccupied = true;
            StartCoroutine(_spawner.ExcludeSpawnPointTemporarily(this));
        }
        else if (other.gameObject.CompareTag(Tag.Player.ToString()) && isOccupied)
        {
            print($"moved object to another spawner: {other.gameObject.name}");
            _spawner.MoveObjectToSpawner(other.gameObject);
        }
        else
        {
            //print($"the collded object: {other.gameObject.name}");
        }
    }
}