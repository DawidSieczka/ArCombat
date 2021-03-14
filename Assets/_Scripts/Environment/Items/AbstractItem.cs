using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractItem : MonoBehaviourPun
{

    private Collider _collider;
    public List<MeshRenderer> MeshRenderer;
    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.Player.ToString()) || other.gameObject.CompareTag(Tag.Enemy.ToString()))
        {
            if (other.gameObject.CompareTag(Tag.Player.ToString()))
                InvokeActionOnPlayer(other);

            OnObjectDestroy();
        }
        else if (other.gameObject.CompareTag(Tag.Bullet.ToString()))
        {
            OnObjectDestroy();
        }
    }

    public abstract void InvokeActionOnPlayer(Collider other);

    private void ComponentsEnabled(bool state)
    {
        _collider.enabled = state;
        MeshRenderer.ForEach(x => x.enabled = state);
    }

    private void OnObjectSpawn()
    {
        ComponentsEnabled(true);
    }

    private void OnObjectDestroy()
    {
        ComponentsEnabled(false);
        StartCoroutine(SpawnObjectAfterTime());
    }

    private IEnumerator SpawnObjectAfterTime()
    {
        yield return new WaitForSeconds(5f);
        OnObjectSpawn();
    }
}