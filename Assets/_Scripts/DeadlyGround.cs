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