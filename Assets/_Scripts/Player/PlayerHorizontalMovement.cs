using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviour{
    JoystickInputs _joystickInputs { get; set; }
    Rigidbody _rb { get; set; }
    float _speed { get; set; } = 60f;

    void Awake(){
        _joystickInputs = FindObjectOfType<JoystickInputs>();
        _rb = GetComponent<Rigidbody>();
    }

    void Update(){
        if (Mathf.Abs(_joystickInputs.xAxis) != 0.2f){
            _rb.velocity = new Vector3(_joystickInputs.xAxis * Time.deltaTime * _speed,_rb.velocity.y,_rb.velocity.z);
        }
    }

}
