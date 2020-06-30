using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyGround : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tag.Player.ToString()))
        {
            GameObject.FindGameObjectWithTag(Tag.Spawner.ToString()).GetComponent<Spawner>().InvokeSpawning();
        }
    }
}
