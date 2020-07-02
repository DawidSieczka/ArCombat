using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerJump : MonoBehaviour{

    Rigidbody _rb;
    PlayerColliders _playerColliders;
    ButtonEvent _jumpButton;
    ParticleSystem _particle;

    public float fallMultiplier;
    public float lowJumpMultiplier;
    public float jumpForce;
    bool _isReadyToJump = true;
    
    void Start(){
        _rb = GetComponent<Rigidbody>();
        _playerColliders = GetComponent<PlayerColliders>();
        _jumpButton = FindObjectOfType<ButtonEvent>();
        _jumpButton.OnJumped.AddListener(Jump);
        _particle = GetComponentInChildren<ParticleSystem>();
        
    }

    void Update(){
        FallingController();
    }


    public bool IsColliding(){
        if (_playerColliders._hitInfoLeft.collider == null && _playerColliders._hitInfoRight.collider == null){
            return false;
        }
        else{
            return true;
        }
    }

    public void Jump()
    {
        Debug.Log($"Is player colliding ground: {IsColliding()}");
        if (_isReadyToJump && IsColliding())
        {
            _particle.Play();
            _isReadyToJump = false;
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            StartCoroutine(JumpCoolDown());
        }
    }

    void FallingController(){
        if (_rb.velocity.y < 0){
            _rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rb.velocity.y > 0){
            _rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    IEnumerator JumpCoolDown(){
     
        yield return new WaitForSeconds(.1f);
        _isReadyToJump = true;

    }

}
