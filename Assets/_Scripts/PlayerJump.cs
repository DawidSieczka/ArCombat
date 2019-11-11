using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerJump : Observerable
{
    [HideInInspector]
    public bool IsColliding {
        get
        {
            return _isColliding;
        }
        set
        {
            Notify();
            _isColliding = value;
        }
    }

    private Rigidbody _rb;
    private JumpButton _jumpButton;
    private PlayerColliders _playerColliders;
    
    private float _fallMultiplier = 2f;
    private float _lowJumpMultiplier = 1.3f;
    private float _jumpForce = 3f;
    private bool _isReadyToJump = true;
    private bool _isColliding;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _jumpButton = FindObjectOfType<JumpButton>();
        _playerColliders = GetComponent<PlayerColliders>();
        AddObserver();
    }

    void Update()
    {
        Jump();
        FallingController();
    }

    private void Jump()
    {
        
        if (_playerColliders._hitInfoLeft.collider == null && _playerColliders._hitInfoRight.collider == null)
        {
            IsColliding = false;
            return;
        }
        else
        {
            IsColliding = true;
        }

        if (_jumpButton.Jump && IsColliding && _isReadyToJump)
        {
            IsColliding = false;
            _isReadyToJump = false;
            _rb.AddForce(Vector3.up * _jumpForce /* * Time.deltaTime*/, ForceMode.Impulse);
            StartCoroutine(JumpCoolDown());
        }
    }

    private void FallingController()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rb.velocity.y > 0 && !_jumpButton.Jump)
        {
            _rb.velocity += Vector3.up * Physics.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private IEnumerator JumpCoolDown()
    {
        yield return new WaitForSeconds(0.1f);
        _isReadyToJump = true;
    }
}
