using ExitGames.Client.Photon;
using Photon.Pun;
using System;
using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviourPun
{
    PlayerColliders _playerColliders;
    public float speedMultiplayer;
    public static int direction = 1;
    public Camera ARcam;
    private SideDetector _sideDetector;
    Rigidbody rb;
    
    private const byte HORIZONTALMOVE_EVENT = 1;

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
        if (Math.Abs(rb.velocity.y) < .2f && base.photonView.IsMine)
        {
            rb.velocity = new Vector3(direction * Time.deltaTime * playerSpeed * speedMultiplayer, rb.velocity.y, rb.velocity.z);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.contacts.Length > 0 && base.photonView.IsMine)
        {
            print("styka sie");
            rb.velocity = Vector3.zero;
        }
    }   
}