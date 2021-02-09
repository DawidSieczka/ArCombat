using Photon.Pun;
using System.Collections;
using UnityEngine;

public class HPPointBehaviour : MonoBehaviourPun
{
    public int IncreasedHP { get; set; } = 25;
    private Collider _collider;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.Player.ToString()) || other.gameObject.CompareTag(Tag.Enemy.ToString()))
        {
            if (other.gameObject.CompareTag(Tag.Player.ToString()))
                other.GetComponent<PlayerHP>().IncreaseHP(IncreasedHP);

            OnObjectDestroy();
        }
        else if (other.gameObject.CompareTag(Tag.Bullet.ToString()))
        {
            OnObjectDestroy();
        }
    }

    private void ComponentsEnabled(bool state)
    {
        _collider.enabled = state;
        _meshRenderer.enabled = state;
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