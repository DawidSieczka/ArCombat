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


    public void Move(float playerSpeed)
    {
        transform.localPosition += transform.TransformDirection(Vector3.right * Time.deltaTime * playerSpeed * 4);

    }

}