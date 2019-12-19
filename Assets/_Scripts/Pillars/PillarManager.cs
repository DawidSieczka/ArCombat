using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarManager : MonoBehaviour{

    [HideInInspector]
    public float AngleRotate;
    [HideInInspector]
    public bool EnabledRotation { get; set; }
    [HideInInspector]
    public bool IsAngleForLeftRot;
    [HideInInspector]
    public bool IsAngleForRightRot;

    Vector3 _startPillarRotationPosition;
    Vector3 _endPillarRotationPosition;
    Vector3 _directionOfRotate;
    RotateButton _rotateButton;
    GameObject _theNearestPillar;
    PillarDetector _pillarDetector;
    PlayerColliders _playerColliders;

    void Start(){
        _pillarDetector = FindObjectOfType<PillarDetector>();
        _rotateButton = FindObjectOfType<RotateButton>();
        _playerColliders = FindObjectOfType<PlayerColliders>();
        _rotateButton.OnRotated += InvokeRotation;
    }

    void Update(){
        if (EnabledRotation){
            RotatePillar();
        }
    }

    void InvokeRotation(object s,EventArgs e){
        var isReadyToRotate = (_rotateButton.Pressed && !EnabledRotation && _playerColliders.IsOnEdge);
        if (isReadyToRotate){
            _theNearestPillar = _pillarDetector.GetTheNearestPillar();
            _startPillarRotationPosition = _theNearestPillar.transform.eulerAngles;
            SetAngleToRotate();
            Debug.Log($"AngleRotate:{AngleRotate}");
            var ValueOfAngle = Vector3.up * AngleRotate;
            _endPillarRotationPosition = _startPillarRotationPosition + ValueOfAngle;
            Debug.Log($"Rotate value is: {ValueOfAngle}");
            ValidateValueOfRotation();
            EnabledRotation = true;
            Debug.Log($"My the nearest pillar is: {_theNearestPillar.gameObject.name}");
            Debug.Log($"Rotation works");
        }
    }

    void RotatePillar(){
        if (Math.Abs(_endPillarRotationPosition.y - _theNearestPillar.transform.eulerAngles.y) < 1f){
            _theNearestPillar.transform.eulerAngles = _endPillarRotationPosition;
            EnabledRotation = false;
        }
        else{
            _theNearestPillar.transform.Rotate(_directionOfRotate * Time.deltaTime, Space.Self) ;
        }
    }
    
    void ValidateValueOfRotation(){

        bool isIncorrectRotaryPosition = _endPillarRotationPosition.y < 0;
        if (isIncorrectRotaryPosition){
            _endPillarRotationPosition.y += 360;
            _startPillarRotationPosition.y += 360;
        }
    }
    public void SetAngleToRotate(){
        if (IsAngleForLeftRot && !IsAngleForRightRot){
            _directionOfRotate = new Vector3(0, 0, 50);
            AngleRotate = -90;
        }else if (!IsAngleForLeftRot && IsAngleForRightRot){
            AngleRotate = 90;
            _directionOfRotate = new Vector3(0,0,-50);
        }
        else
        {
            AngleRotate = 0;
        }
    }

}
