using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPositionSetter : MonoBehaviour
{
    private ShootButton _shootButton;
    private Rigidbody _rb;

    void Start()
    {
        _shootButton = FindObjectOfType<ShootButton>();
        _rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        ResetPosition();   
    }
    private void ResetPosition()
    {
        if (_shootButton.reset)
        {
            transform.localPosition = new Vector3(0.359f, 0.1384f, 1.049f);
            _rb.velocity = Vector3.zero;
        }
    }
}
