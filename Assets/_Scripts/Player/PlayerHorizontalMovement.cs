using System;
using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviour
{
    public float speed = 1.2f;
    public static int direction;
    public Camera ARcam;
    private SideDetector _sideDetector;

    private void Start()
    {
        _sideDetector = GameObject.FindObjectOfType<SideDetector>();
    }

    private void Update()
    {
        CheckCameraAndPlayerDirection();

        //Debug
        if (Debug.isDebugBuild && false) //Smth not working with horizontal GetAxis
            DebugMoveComp();
        else
            MobileMovement();
    }

    private void CheckCameraAndPlayerDirection()
    {
        if (_sideDetector.CurrentSide == side.back)
            direction *= -1;
    }

    private void MobileMovement()
    {
        transform.localPosition += transform.TransformDirection(Vector3.right * Time.deltaTime * direction * speed);
    }

    private void DebugMoveComp()
    {
        var _axisX = Input.GetAxis("Debug Horizontal");

        if (Math.Abs(_axisX) > 0)
        {
            transform.localPosition += transform.TransformDirection(Vector3.right * Time.deltaTime * _axisX * direction * speed);
        }
    }
}