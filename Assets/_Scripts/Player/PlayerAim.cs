using Photon.Pun;
using System;
using UnityEngine;

public class PlayerAim : MonoBehaviourPun
{
    [SerializeField]
    private GameObject _aimingLinePrefab;

    private GameObject _aimingLine;
    private Vector3 _playerPos;
    public bool IsZDepthAxis = true;

    [HideInInspector]
    public Vector3? AimedTargetPosition;

    private Renderer _aimingLineShaderRenderer;
    private PlayerCameraCenter _playerCameraCenter;
    private const int _groundLayer = 11;
    private const int _spawnLayer = 13;

    private void Awake()
    {
        SpawnAimingLine();
    }

    private void SpawnAimingLine()
    {
        if (base.photonView.IsMine)
        {
            _aimingLine = Instantiate(_aimingLinePrefab, Vector3.zero, Quaternion.identity);
            _aimingLineShaderRenderer = _aimingLine.GetComponent<Renderer>();
            _playerCameraCenter = Camera.main.GetComponentInChildren<PlayerCameraCenter>();
        }
    }

    //invoked only from Player Rotation button
    public void ChangeDepthAxis()
    {
        if (IsZDepthAxis)
            IsZDepthAxis = false;
        else
            IsZDepthAxis = true;
    }

    private void Update()
    {
        if (_aimingLine == null)
            return;

        SetAimingPoint();
        SetAimingLine();
    }

    private void SetAimingLine()
    {
        _playerPos = transform.position;
        var _xAxisMiddlePoint = (AimedTargetPosition.Value.x - _playerPos.x) / 2;
        var _zAxisMiddlePoint = (AimedTargetPosition.Value.z - _playerPos.z) / 2;
        var _yAxisMiddlePoint = (AimedTargetPosition.Value.y - _playerPos.y) / 2;

        var _direction = (transform.position - AimedTargetPosition.Value);
        var scaleY = Vector3.Distance(AimedTargetPosition.Value, _playerPos) * 0.5f;
        _aimingLineShaderRenderer.material.SetFloat("_distanceScaleValue", scaleY);

        if (IsZDepthAxis)
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

        _aimingLine.transform.localScale = new Vector3(_aimingLine.transform.localScale.x, scaleY, _aimingLine.transform.localScale.z);
    }

    private void SetAimingPoint()
    {
        try
        {
            _playerCameraCenter.SetDepthToPlayersPosition(IsZDepthAxis, transform.position.x, transform.position.z);
            var pointingDirection = _playerCameraCenter.transform.position;

            Vector3 raycastDirection = GetNormalizedRaycastDirection(pointingDirection);
            Debug.DrawRay(transform.position, raycastDirection, Color.green);
            var rayDistance = Vector3.Distance(pointingDirection, transform.position);
            var isHit = Physics.Raycast(transform.position, raycastDirection, out RaycastHit hit, rayDistance);

            if (isHit && isHittingCorrectObjects(hit))
            {
                Debug.Log($"Hitting object: {hit.collider.gameObject.name}");

                AimedTargetPosition = hit.point;
            }
            else
            {
                AimedTargetPosition = pointingDirection;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }

    private bool isHittingCorrectObjects(RaycastHit hit)
    {
        return hit.collider.gameObject.layer != _groundLayer
            && hit.collider.gameObject.tag != Tag.Bullet.ToString()
            && hit.collider.gameObject.layer != _spawnLayer;
    }

    private Vector3 GetNormalizedRaycastDirection(Vector3 pointingDirection)
    {
        var x = pointingDirection.x - transform.position.x;
        var y = pointingDirection.y - transform.position.y;
        var z = pointingDirection.z - transform.position.z;
        return (new Vector3(x, y, z)).normalized;
    }
}