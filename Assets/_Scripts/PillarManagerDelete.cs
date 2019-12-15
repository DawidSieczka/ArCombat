using System;
using System.Linq;
using UnityEngine;

public class PillarManagerDelete : MonoBehaviour{
    [HideInInspector]
    public GameObject[] Pillars;
    [HideInInspector]
    public GameObject TheNearestPillar;
    [HideInInspector]
    public bool IsRotating;
    [HideInInspector]
    public bool WantRotate;
    [HideInInspector]
    public float AngleRotate;

    GameObject _player;
    JoystickInputs _joystickInputs;
    PlayerColliders _playerColliders;
    Vector3 _startPillarRotationPosition;
    Vector3 _endPillarRotationPosition;

    
    void Start(){
        Pillars = GameObject.FindGameObjectsWithTag(Tag.PillarTag.ToString());
        _joystickInputs = GameObject.FindObjectOfType<JoystickInputs>();
        _playerColliders = GameObject.FindObjectOfType<PlayerColliders>();
        //TODO Refactor class PlayerColliders - IsOnEdge shouldn't be in this class (change name / add new class)

        _player = GameObject.FindGameObjectWithTag(Tag.Player.ToString());
    }

    void Update(){

        //TODO refactor input to other script - create script input mangaer / input controller (check event - maybe better)
        var isInputDown = _joystickInputs.yAxis < -0.6f;
        if (isInputDown && !IsRotating && !WantRotate && _playerColliders.IsOnEdge){
            InvokeRotation();
        }
    }

    void InvokeRotation(){
        ChooseTheNearestPillar();
        IsRotating = true;
        WantRotate = true;
        _startPillarRotationPosition = TheNearestPillar.transform.eulerAngles;

        var valueOfWantedValue = Vector3.up * AngleRotate;
        _endPillarRotationPosition = _startPillarRotationPosition + valueOfWantedValue;
        
        CheckPositiveValueOfRotation();
    }

    void CheckPositiveValueOfRotation(){
        bool isIncorrectRotaryPosition = _endPillarRotationPosition.y < 0;
        if (isIncorrectRotaryPosition){
            _endPillarRotationPosition.y += 360;
            _startPillarRotationPosition.y += 360;
        }
    }

    public void ChooseTheNearestPillar(){
        var previousDistance = float.MaxValue;

        //Amount of Pillars is about 10-30 (not too much)
        foreach (var pillar in Pillars){
            var distPlayerPillar = Vector3.Distance(pillar.transform.position, _player.transform.position);

            if (distPlayerPillar < previousDistance){
                previousDistance = distPlayerPillar;
                TheNearestPillar = pillar;
            }
        }
    }

    
}
