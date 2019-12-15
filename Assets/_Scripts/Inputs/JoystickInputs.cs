using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JoystickInputs : MonoBehaviour{
    [HideInInspector] public float xAxis;
    [HideInInspector] public float yAxis;
    
    private Joystick _joystick;

    void Start(){
        _joystick = FindObjectOfType<Joystick>();
    }

    void Update(){
        xAxis = _joystick.Horizontal; 
        yAxis = _joystick.Horizontal;
    }
}
