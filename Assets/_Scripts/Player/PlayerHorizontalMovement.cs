using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviour{

    public bool isDebug;

    Rigidbody _rb { get; set; }
    float _speed { get; set; } =  1.2f;
    ButtonEvent _buttonEvent;
    public static int direction;
    void Start(){
        _rb = GetComponent<Rigidbody>();
        _buttonEvent = FindObjectOfType<ButtonEvent>();

    }

    void Update(){
        if (isDebug)
            DebugMoveComp();
        else
            MobileMovement(direction);
    }

    void MobileMovement(int direction){
            transform.localPosition += transform.TransformDirection(Vector3.right * Time.deltaTime * direction * _speed);
    }

    void DebugMoveComp(){
        var _axisX = Input.GetAxis("Horizontal");
        if (Math.Abs(_axisX) > 0) 
            transform.localPosition += transform.TransformDirection(Vector3.right * Time.deltaTime * _axisX * _speed);
    }

}
