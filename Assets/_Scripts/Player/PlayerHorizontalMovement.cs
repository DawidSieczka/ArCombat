using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviour{

    public bool isDebug;

    JoystickInputs _joystickInputs { get; set; }
    Rigidbody _rb { get; set; }
    float _speed { get; set; } =  1.2f;

    void Awake(){
        _joystickInputs = FindObjectOfType<JoystickInputs>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isDebug)
        {
            DebugMoveComp();
        }
        else
        {
            MobileMovement();
        }

        //transform.InverseTransformDirection(transform.position += Vector3.right * _joystickInputs.xAxis * Time.deltaTime * _speed);
    }

    private void MobileMovement(){
        bool IsJoystickMoved = Mathf.Abs(_joystickInputs.xAxis) >= 0.2f;
        if (IsJoystickMoved)
            transform.localPosition += transform.TransformDirection(Vector3.right * Time.deltaTime * _joystickInputs.xAxis * _speed);

        Debug.Log($"X value of Joystick is: {_joystickInputs.xAxis}");
    }

    void DebugMoveComp(){
        var _axisX = Input.GetAxis("Horizontal");
        if (Math.Abs(_axisX) > 0) 
            transform.localPosition += transform.TransformDirection(Vector3.right * Time.deltaTime * _axisX * _speed);
    }

}
