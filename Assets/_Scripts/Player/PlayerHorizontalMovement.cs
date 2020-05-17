using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviour{

    public bool isDebug;

    public float speed =  1.2f;
    public static int direction;
    public Camera ARcam;
    SideDetector _sideDetector;
    void Start()
    {
        _sideDetector = GameObject.FindObjectOfType<SideDetector>();   
    }
    void Update(){
        CheckCameraAndPlayerDirection();

        if (isDebug)
            DebugMoveComp();
        else
            MobileMovement();
    }
    void CheckCameraAndPlayerDirection(){
        if (_sideDetector.CurrentSide == side.back)
            direction *= -1;
    }

    void MobileMovement(){
            transform.localPosition += transform.TransformDirection(Vector3.right * Time.deltaTime * direction * speed);
    }

    void DebugMoveComp(){
        var _axisX = Input.GetAxis("Horizontal");

        if (Math.Abs(_axisX) > 0){
            transform.localPosition += transform.TransformDirection(Vector3.right * Time.deltaTime * _axisX*direction* speed);

        }
    }

}
