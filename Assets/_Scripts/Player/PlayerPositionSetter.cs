using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerPositionSetter : MonoBehaviour{
    ShootButton _shootButton;
    Rigidbody _rb;

    void Start(){
        _shootButton = FindObjectOfType<ShootButton>();
        _rb = GetComponent<Rigidbody>();
        _shootButton.OnShooted += ResetPosition;
    }

    void ResetPosition(object s,EventArgs e){
        transform.localPosition = new Vector3(0.359f, 0.1384f, 1.049f);
        _rb.velocity = Vector3.zero;
    }
}
