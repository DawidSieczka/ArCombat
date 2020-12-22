using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviourPun
{
    private Rigidbody _rb;
    private PlayerColliders _playerColliders;
    private ButtonEvent _jumpButton;
    private TouchScreen _touchScreen;
    private ParticleSystem _particle;

    public float fallMultiplier;
    public float lowJumpMultiplier;
    public float jumpForce;
    private bool _isReadyToJump = true;
    private const byte JUMP_EVENT = 2;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerColliders = GetComponent<PlayerColliders>();
        _touchScreen = FindObjectOfType<TouchScreen>();
        _touchScreen.OnJumped.AddListener(Jump);
        _particle = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        FallingController();
    }

    public bool IsColliding()
    {
        if (_playerColliders._hitInfoLeft.collider == null && _playerColliders._hitInfoRight.collider == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Jump()
    {
        Debug.Log($"Is player colliding ground: {IsColliding()}");
        if (_isReadyToJump && IsColliding() && base.photonView.IsMine)
        {
            _particle.Play();
            _isReadyToJump = false;
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            StartCoroutine(JumpCoolDown());

        }
    }

    void NetworkingClient_EventReceived(EventData obj)
    {
        if (obj.Code == JUMP_EVENT)
            _rb.velocity = (Vector3)obj.CustomData;
    }

    private void FallingController()
    {
        if (base.photonView.IsMine)
        {
            if (_rb.velocity.y < 0)
            {
                _rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
            }
            else if (_rb.velocity.y > 0)
            {
                _rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
            }

        }
    }

    private IEnumerator JumpCoolDown()
    {
        yield return new WaitForSeconds(.1f);
        _isReadyToJump = true;
    }

    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClient_EventReceived;
    }

    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= NetworkingClient_EventReceived;
    }

}