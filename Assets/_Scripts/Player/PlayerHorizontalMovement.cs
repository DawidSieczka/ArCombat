using System;
using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviour
{
    PlayerColliders _playerColliders;
    public float speedMultiplayer;
    public static int direction = 1;
    public Camera ARcam;
    private SideDetector _sideDetector;
    Rigidbody rb;
    private void Start()
    {
        _sideDetector = GameObject.FindObjectOfType<SideDetector>();
        _playerColliders = GetComponent<PlayerColliders>();
        rb = GetComponent<Rigidbody>();
    }

    private void CheckCameraAndPlayerDirection()
    {
        if (_sideDetector.CurrentSide == side.back)
            direction *= -1;
    }

    public void Move(float playerSpeed)
    {
        if (Math.Abs(rb.velocity.y) < .2f) 
        rb.velocity = new Vector3(direction * Time.deltaTime * playerSpeed * speedMultiplayer, rb.velocity.y, rb.velocity.z);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.contacts.Length > 0)
        {
            print("styka sie");
            rb.velocity = Vector3.zero;

        }
    }
    private void LateUpdate()
    {
        if (_playerColliders._hitInfoLeft.collider && _playerColliders._hitInfoRight.collider) {
        }

    }
}