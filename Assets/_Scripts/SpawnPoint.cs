using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public void SpawnPlayer(GameObject Player)
    {
        var rb = Player.GetComponent<Rigidbody>();
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        Player.transform.localEulerAngles = Vector3.zero;
        Player.transform.position = this.gameObject.transform.position;
        Player.SetActive(true);
    }
}