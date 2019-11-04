using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    private Joystick _joystick;
    private JoyButton _joyButton;
    [HideInInspector] public float xAxis;
    [HideInInspector] public float yAxis;

    void Start()
    {
        _joystick = FindObjectOfType<Joystick>();
        _joyButton = FindObjectOfType<JoyButton>();
    }

    void Update()
    {
        xAxis = _joystick.Horizontal; 
        yAxis = _joystick.Horizontal;

    }
}
