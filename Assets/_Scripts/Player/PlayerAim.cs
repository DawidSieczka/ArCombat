using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    [SerializeField]
    GameObject _aimingLinePrefab;
    GameObject _aimingLine;
    Vector3 _playerPos;
    bool _isZDepthAxis = true;
    bool _isPointingAtTarget;
    Vector3? _aimedTargetPosition;
    void Awake()
    {
        //spawn _aimingLine
        _aimingLine = Instantiate(_aimingLinePrefab, Vector3.zero , Quaternion.identity);
    }

    //invoked only from Player Rotation button
    public void ChangeDepthAxis()
    {
        if (_isZDepthAxis)
        {
            _isZDepthAxis = false;
        }
        else
        {
            _isZDepthAxis = true;
        }
    }

    public void SetIsPlayerPointingTarget(Vector3? aimedTargetPosition)
    {
        if (aimedTargetPosition != null)
        {
            _aimedTargetPosition = aimedTargetPosition;
            _isPointingAtTarget = true;
            _aimingLine.SetActive(true);
            print("wlacz");
        }
        else
        {
            _isPointingAtTarget = false;
            _aimingLine.SetActive(false);
            print("wylacz");

        }
    }

    void Update()
    {
        if (_isPointingAtTarget)
        {
            //
            //  Setting aiming line
            //

            //get player pos
            _playerPos = transform.position;
            //get middle of screen
           
            //wypusc raycast Camera.Forward


            //jezeli trafiles w kolizje z tagiem player  narysuj linie celowania
            //jezeli nie return;

            //calculate middle position between player and middle of the screen
            var _xAxisMiddlePoint = (_aimedTargetPosition.Value.x - _playerPos.x) / 2;
            var _zAxisMiddlePoint = (_aimedTargetPosition.Value.z - _playerPos.z) / 2;
            var _yAxisMiddlePoint = (_aimedTargetPosition.Value.y - _playerPos.y) / 2;

            //set _aimingLine between them
            if (_isZDepthAxis)
            {
                _aimingLine.transform.position = new Vector3(_playerPos.x + _xAxisMiddlePoint, _playerPos.y + _yAxisMiddlePoint, _playerPos.z);
            }
            else
            {
                _aimingLine.transform.position = new Vector3(_playerPos.x, _playerPos.y + _yAxisMiddlePoint, _playerPos.z + _zAxisMiddlePoint);
            }
            //
            //  Rotation of aiming line
            //

            //
            //  Getting collisions on the aiming line way
            //


            //
            //   Stretching
            //


        }
    }
}
