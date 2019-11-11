using System;
using System.Linq;
using UnityEngine;

class PillarManager : MonoBehaviour
{
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

    private GameObject _player;
    private JoystickInputs _joystickInputs;
    private PlayerColliders _playerColliders;
    private Vector3 _startPillarRotation;
    private Vector3 _endPillarRotation;

    
    void Start()
    {
        Pillars = GameObject.FindGameObjectsWithTag(Tag.PillarTag.ToString());
        _joystickInputs = GameObject.FindObjectOfType<JoystickInputs>();
        _playerColliders = GameObject.FindObjectOfType<PlayerColliders>();
        _player = GameObject.FindGameObjectWithTag(Tag.Player.ToString());
    }

    void Update()
    {
        var isInputDown = _joystickInputs.yAxis < -0.6f;
        if (isInputDown && !IsRotating && !WantRotate && _playerColliders.IsOnEdge)
        {
            InvokeRotation();
        }
    }

    private void InvokeRotation()
    {
        ChooseTheNearestPillar();
        IsRotating = true;
        WantRotate = true;
        _startPillarRotation = TheNearestPillar.transform.eulerAngles;
        _endPillarRotation = _startPillarRotation + Vector3.up * AngleRotate;

        if (_endPillarRotation.y < 0)
        {
            _endPillarRotation.y += 360;
            _startPillarRotation.y += 360;
        }
    }

    internal void ChooseTheNearestPillar()
    {
        var previousDistance = float.MaxValue;
        foreach (var pillar in Pillars)
        {
            var distPlayerPillar = Vector3.Distance(pillar.transform.position, _player.transform.position);

            if (distPlayerPillar < previousDistance)
            {
                previousDistance = distPlayerPillar;
                TheNearestPillar = pillar;
            }
        }
    }

    
}
