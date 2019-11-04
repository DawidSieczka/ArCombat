using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviour
{
    private PlayerInputs _playerInputs { get; set; }
    private Rigidbody _rb { get; set; }
    private float _speed { get; set; } = 60f;
    void Awake()
    {
        _playerInputs = FindObjectOfType<PlayerInputs>();
        _rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (Mathf.Abs(_playerInputs.xAxis) != 0.2f)
        {
            _rb.velocity = new Vector3(_playerInputs.xAxis * Time.deltaTime * _speed,_rb.velocity.y,_rb.velocity.z);
        }
    }

}
