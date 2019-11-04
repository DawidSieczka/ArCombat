using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float fallMultiplier = 20f;
    public float lowJumpMultiplier = 15f;
    private Rigidbody _rb;
    private bool isColliding;
    private PlayerInputs _playerInputs { get; set; }
    private JoyButton jb;
    private RaycastHit _hitInfoLeft;
    private RaycastHit _hitInfoRight;
    private bool _jumped;
    private int _layerMaskGround = 1<<9;
    private bool _isReadyToJump = true;
    private float jumpForce = 500f;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInputs = FindObjectOfType<PlayerInputs>();
        jb = FindObjectOfType<JoyButton>();
    }
    void Update()
    {
        Jump();
        ResetPosition();
    }

    private void ResetPosition()
    {
        if (jb.Pressed)
        {
            transform.localPosition = new Vector3(0.29f, .7f, -.265f);
            _rb.velocity = Vector3.zero;
        }
    }
    private void Jump()
    {
        var hitGround1 = Physics.Raycast(transform.localPosition + new Vector3(-0.03f, 0, 0), Vector3.down,
            out _hitInfoLeft, 1, _layerMaskGround);
        var hitGround2 = Physics.Raycast(transform.localPosition + new Vector3(0.03f, 0, 0), Vector3.down,
            out _hitInfoRight, 1, _layerMaskGround);
        
        if (_hitInfoLeft.collider == null && _hitInfoRight.collider == null)
        {

            isColliding = false;
            return;
        }
        else
        {
            print(_hitInfoLeft.collider.name);
            print(_hitInfoRight.collider.name);
            isColliding = true;
        }

        if ((_playerInputs.yAxis>.1f) && isColliding && _isReadyToJump)
        {
            print("dzialaaaaaaaaaaa");
            isColliding = false;
            _isReadyToJump = false;
            _rb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
            StartCoroutine(JumpCoolDown());
        }
    }
    private void FallingController()
    {
        if (_rb.velocity.y < 0)
        {
            _rb.velocity += new Vector3(0,_playerInputs.yAxis,0) * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_rb.velocity.y > 0 && !(_playerInputs.yAxis>0.1f))
        {
            _rb.velocity += new Vector3(0, _playerInputs.yAxis, 0) * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
    private IEnumerator JumpCoolDown()
    {
        yield return new WaitForSeconds(0.1f);
        _isReadyToJump = true;
    }
}
