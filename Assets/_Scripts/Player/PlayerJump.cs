using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerJump : MonoBehaviour{

    Rigidbody _rb;
    PlayerColliders _playerColliders;
    ButtonEvent newJumpButton;

    public float _fallMultiplier = 2f;
    public float _lowJumpMultiplier = 1.3f;
    public float _jumpForce = 5f;
    bool _isReadyToJump = true;
    
    void Start(){
        _rb = GetComponent<Rigidbody>();
        _playerColliders = GetComponent<PlayerColliders>();
        newJumpButton = FindObjectOfType<ButtonEvent>();
        newJumpButton.OnJumped.AddListener(Jump);
    }

    void Update(){
       // FallingController();
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
        // Debug.Log($"Is player colliding ground: {IsColliding()}");
        if (_isReadyToJump){
            _isReadyToJump = false;
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.VelocityChange);
            StartCoroutine(JumpCoolDown());
        }
    }

    void FallingController(){
        if (_rb.velocity.y < 0){
            _rb.velocity += Vector3.up * Physics.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rb.velocity.y > 0){
            _rb.velocity += Vector3.up * Physics.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    IEnumerator JumpCoolDown(){
        yield return new WaitForSeconds(0.1f);
        _isReadyToJump = true;
    }
}
