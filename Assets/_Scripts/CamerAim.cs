using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerAim : MonoBehaviour
{
    int _layerMask = 1 << 10;
    RaycastHit[] hits;
    PlayerAim _playerAim;
    void Start()
    {
        _playerAim = FindObjectOfType<PlayerAim>();
    }

    void Update()
    {
        hits = Physics.RaycastAll(transform.position, transform.TransformDirection(Vector3.forward), Mathf.Infinity,_layerMask);
        //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hits[0].distance, Color.yellow);
        _playerAim.SetIsPlayerPointingTarget(hits[0].point);
    }
}
