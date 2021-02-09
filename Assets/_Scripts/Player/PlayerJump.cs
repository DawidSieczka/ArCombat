using Photon.Pun;
using System.Collections;
using UnityEngine;

public class PlayerJump : MonoBehaviourPun
{
    private Rigidbody _rb;
    private PlayerColliders _playerColliders;
    private ButtonEvent _jumpButton;
    private TouchScreen _touchScreen;
    private ParticleSystem _particle;

    public float FallMultiplier;
    public float LowJumpMultiplier;
    public float JumpForce;
    private bool _isReadyToJump = true;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerColliders = GetComponent<PlayerColliders>();
        _touchScreen = FindObjectOfType<TouchScreen>();
        _touchScreen.OnJumped.AddListener(Jump);
        _particle = GetComponentInChildren<ParticleSystem>();
    }

    public bool IsColliding()
    {
        if (_playerColliders.HitInfoLeft.collider == null && _playerColliders.HitInfoRight.collider == null)
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
            //transform.position += Vector3.up * jumpForce * Time.deltaTime;

            _rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
            //transform.position = transform.TransformDirection(new Vector3(transform.position.x, jumpForce, transform.position.z));

            StartCoroutine(JumpCoolDown());
        }
    }

    private IEnumerator JumpCoolDown()
    {
        yield return new WaitForSeconds(.1f);
        _isReadyToJump = true;
    }
}