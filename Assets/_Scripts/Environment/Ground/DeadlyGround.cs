using UnityEngine;

public class DeadlyGround : MonoBehaviour
{
    private GameObject _player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.Player.ToString()))
        {
            GameObject.FindGameObjectWithTag(Tag.Spawner.ToString()).GetComponent<PlayersSpawner>().MoveObjectToSpawner(other.gameObject);
        }
    }

    private void LateUpdate()
    {
        InitIfDoesntExist();
        if (_player != null)
        {
            if (_player.transform.position.y < (transform.position.y - 20) || _player.transform.position.y > 20)
            {
                GameObject.FindGameObjectWithTag(Tag.Spawner.ToString()).GetComponent<PlayersSpawner>().MoveObjectToSpawner(_player);
            }
        }
    }

    private void InitIfDoesntExist()
    {
        if (_player == null)
            _player = GameObject.FindGameObjectWithTag(Tag.Player.ToString());
    }
}