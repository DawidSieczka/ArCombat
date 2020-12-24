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

    [SerializeField] [Range(0, 1)] float LerpConstant;
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
        if (base.photonView.IsMine)
        {
            transform.position += Vector3.right * speedMultiplayer * direction * playerSpeed * Time.deltaTime; 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.contacts.Length > 0 && base.photonView.IsMine)
        {
            rb.velocity = Vector3.zero;
        }
    }   
}