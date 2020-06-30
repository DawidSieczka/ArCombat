using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    
    public void SpawnPlayer(GameObject Player)
    {
        Player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        Player.transform.position = this.gameObject.transform.position;
        Player.SetActive(true);
    }
}
