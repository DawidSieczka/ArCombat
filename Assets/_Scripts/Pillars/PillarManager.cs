using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarManager : MonoBehaviour{
    [HideInInspector]
    public bool isAngleForLeftRot;
    [HideInInspector]
    public bool isAngleForRightRot;
    PillarRotationValidator _correctness;
    PillarDetector _pillarDetector;
    Vector3 _startPillarRotationPosition;
    Vector3 _endPillarRotationPosition;
    RotateButton _rotateButton;
    PlayerColliders _playerColliders;
    Vector3 DirectionOfRotate;

    [HideInInspector]
    public float AngleRotate;
    [HideInInspector]
    public bool EnabledRotation;

    GameObject _TheNearestPillar;

    void Start(){
        _correctness = FindObjectOfType<PillarRotationValidator>();
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
            _TheNearestPillar = _pillarDetector.ChooseTheNearestPillar();
            _startPillarRotationPosition = _TheNearestPillar.transform.eulerAngles;
            SetAngleToRotate();
            Debug.Log($"AngleRotate:{AngleRotate}");
            var ValueOfAngle = Vector3.up * AngleRotate; //tutaj jest błąd! !! !!!
            _endPillarRotationPosition = _startPillarRotationPosition + ValueOfAngle;
            Debug.Log($"wartosc obrotu to: {ValueOfAngle}");
            ValidateValueOfRotation();
            EnabledRotation = true;
            Debug.Log($"My the nearest pillar is: {_TheNearestPillar.gameObject.name}");
            Debug.Log($"Rotation works");
        }
    }

    void RotatePillar(){
        if (Math.Abs(_endPillarRotationPosition.y - _TheNearestPillar.transform.eulerAngles.y) < 1f){
            _TheNearestPillar.transform.eulerAngles = _endPillarRotationPosition;
            EnabledRotation = false;
        }
        else{
            _TheNearestPillar.transform.Rotate(DirectionOfRotate * Time.deltaTime, Space.Self) ;
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
        if (isAngleForLeftRot && !isAngleForRightRot){
            DirectionOfRotate = new Vector3(0, 0, 50);
            AngleRotate = -90;
        }else if (!isAngleForLeftRot && isAngleForRightRot){
            AngleRotate = 90;
            DirectionOfRotate = new Vector3(0,0,-50);
        }
        else
        {
            AngleRotate = 0;
        }
    }

}
