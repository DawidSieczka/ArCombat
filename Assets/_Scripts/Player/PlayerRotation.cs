using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private PlayerPositionCorrector _posCorrector;
    private PlayerAim _playerAim;
    private ButtonEvent _buttonEvent;
    private SideDetector _sideDetector;
    private bool _shouldRotate;
    private float _startRotationPos;
    private float _endRotationPos;
    public float RotationSpeed;

    private void Start()
    {
        _buttonEvent = FindObjectOfType<ButtonEvent>();
        _buttonEvent.OnRotated.AddListener(InvokeRotation);
        _posCorrector = GetComponent<PlayerPositionCorrector>();
        _playerAim = GetComponent<PlayerAim>();
        _sideDetector = GetComponent<SideDetector>();
    }

    private void Update()
    {
        if (_shouldRotate)
        {
            transform.Rotate(Vector3.up, RotationSpeed * Time.deltaTime);

            if (Math.Abs(transform.eulerAngles.y - _endRotationPos) < 3f)
            {
                _shouldRotate = false;
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, _endRotationPos, transform.eulerAngles.z);
            }
        }
    }

    private void InvokeRotation()
    {
        if (!_shouldRotate) //You can click Invoke only when player is not rotating
        {
            _shouldRotate = true;
            _startRotationPos = transform.rotation.eulerAngles.y;
            var targetAngle = (float)_startRotationPos + 90;
            _endRotationPos = RoundValueToCorrectAngle(targetAngle);
            _posCorrector.CorrectPosition();
            _playerAim.ChangeDepthAxis();
            _sideDetector.ChangeDepthAxis();
        }
    }

    public float RoundValueToCorrectAngle(float inputValue)
    {
        List<float> correctValues = new List<float> { 0, 90, 180, 270, 360 };
        print(inputValue);
        float roundedAngle = 0;

        if (inputValue >= 360)
            return roundedAngle;

        float theSmallestDifeerence = float.MaxValue;
        foreach (var correctValue in correctValues)
        {
            var difference = Math.Abs(inputValue - correctValue);

            bool isTheSmallestDifference = (difference <= theSmallestDifeerence);
            if (isTheSmallestDifference)
            {
                theSmallestDifeerence = difference;
                roundedAngle = correctValue;
            }
        }

        return roundedAngle;
    }
}