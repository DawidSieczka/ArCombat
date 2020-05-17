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
    int _layerMask = 1 << 10;
    RaycastHit[] hits;
    Renderer _aimingLineShaderRenderer;
    void Awake(){
        //spawn _aimingLine
        _aimingLine = Instantiate(_aimingLinePrefab, Vector3.zero , Quaternion.identity);
        _aimingLineShaderRenderer = _aimingLine.GetComponent<Renderer>();
    }

    //invoked only from Player Rotation button
    public void ChangeDepthAxis(){
        if (_isZDepthAxis)
            _isZDepthAxis = false;
        else
            _isZDepthAxis = true;
    }

    bool isTargetStandingInTheSameAxis(Vector3 aimedTargetPosition){
        var _acceptableDifference = 0.5f;
        if (_isZDepthAxis)
            return (Mathf.Abs(transform.position.z - aimedTargetPosition.z) < _acceptableDifference);
        else
            return (Mathf.Abs(transform.position.x - aimedTargetPosition.x) < _acceptableDifference);
    }
    public void SetIsPlayerPointingTarget(Vector3 aimedTargetPosition){
        if (aimedTargetPosition != null && isTargetStandingInTheSameAxis(aimedTargetPosition)){
            _aimedTargetPosition = aimedTargetPosition;
            _isPointingAtTarget = true;
            _aimingLine.SetActive(true);
        }
        else{
            _isPointingAtTarget = false;
            _aimingLine.SetActive(false);
        }
    }

    void Update(){
        hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), Mathf.Infinity, _layerMask); //wsadzic do kamery, dodac interface'y, dodac event na podstawie trafienia
        var isRaycastHitting = (hits.Length > 0);
        if (!isRaycastHitting)
            return;
        else{
            SetIsPlayerPointingTarget(hits[0].point);
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * hits[0].distance, Color.yellow);
        }

        if (_isPointingAtTarget){
            //calculate middle position between player and middle of the screen
            _playerPos = transform.position;
            var _xAxisMiddlePoint = (_aimedTargetPosition.Value.x - _playerPos.x) / 2;
            var _zAxisMiddlePoint = (_aimedTargetPosition.Value.z - _playerPos.z) / 2;
            var _yAxisMiddlePoint = (_aimedTargetPosition.Value.y - _playerPos.y) / 2;

            //
            //  Rotate aiming line and set _aimingLine between target and player
            //
            
            var _direction = (transform.position - _aimedTargetPosition.Value);
            var scaleY = Vector3.Distance(_aimedTargetPosition.Value, _playerPos) * 0.5f;
            _aimingLineShaderRenderer.material.SetFloat("_distanceScaleValue", scaleY);

            if (_isZDepthAxis)
            {
                float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg - 90;
                _aimingLine.transform.position = new Vector3(_playerPos.x + _xAxisMiddlePoint, _playerPos.y + _yAxisMiddlePoint, _playerPos.z);
                _aimingLine.transform.eulerAngles = Vector3.forward * angle;
            }
            else
            {
                float angle = Mathf.Atan2(_direction.y, _direction.z) * Mathf.Rad2Deg - 90;
                _aimingLine.transform.position = new Vector3(_playerPos.x, _playerPos.y + _yAxisMiddlePoint, _playerPos.z + _zAxisMiddlePoint);
                _aimingLine.transform.eulerAngles = Vector3.left * angle;
            }

            
            _aimingLine.transform.localScale = new Vector3(_aimingLine.transform.localScale.x, scaleY, _aimingLine.transform.localScale.z );
            print(_aimingLineShaderRenderer.material.GetFloat("_distanceScaleValue"));
            //
            //  Getting collisions on the aiming line way
            //
        }
    }
}
