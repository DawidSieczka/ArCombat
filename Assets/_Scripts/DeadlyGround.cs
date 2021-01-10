using System.Collections;
using UnityEngine;

public class DeadlyGround : MonoBehaviour
{
    private GameObject _player;
    private bool isPlayerBelowGround;

    private void Start()
    {
        StartCoroutine(LatePlayerInit());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.Player.ToString()))
        {
            GameObject.FindGameObjectWithTag(Tag.Spawner.ToString()).GetComponent<Spawner>().MoveObjectToSpawner(other.gameObject);
        }
    }

    private void Update()
    {
        if (!isPlayerBelowGround)
        {
            if (_player.transform.position.y < transform.position.y || _player.transform.position.y > 20)
            {
                StartCoroutine(CorrectPlayerPositionToSpawner());
            }
        }
    }

    private IEnumerator LatePlayerInit()
    {
        yield return new WaitForSeconds(3);
        _player = GameObject.FindGameObjectWithTag(Tag.Player.ToString());
    }

    private IEnumerator CorrectPlayerPositionToSpawner()
    {
        isPlayerBelowGround = true;
        yield return new WaitForSeconds(4);
        GameObject.FindGameObjectWithTag(Tag.Spawner.ToString()).GetComponent<Spawner>().MoveObjectToSpawner(_player);
        isPlayerBelowGround = false;
    }
}