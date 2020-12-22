using Photon.Pun;
using UnityEngine;

public class PlayerAim : MonoBehaviourPun
{
    [SerializeField]
    private GameObject _aimingLinePrefab;

    private GameObject _aimingLine;
    private Vector3 _playerPos;
    public bool isZDepthAxis = true;
    private bool _isPointingAtTarget;

    [HideInInspector]
    public Vector3? aimedTargetPosition;

    private int _layerMask = 1 << 10;
    private RaycastHit[] hits;
    private Renderer _aimingLineShaderRenderer;
    private SideDetector _sideDetector;

    private void Awake()
    {
        //spawn _aimingLine
        if (base.photonView.IsMine)
        {
            _aimingLine = Instantiate(_aimingLinePrefab, Vector3.zero, Quaternion.identity);
            _aimingLineShaderRenderer = _aimingLine.GetComponent<Renderer>();
            _sideDetector = FindObjectOfType<SideDetector>();
        }
    }

    //invoked only from Player Rotation button
    public void ChangeDepthAxis()
    {
        if (isZDepthAxis)
            isZDepthAxis = false;
        else
            isZDepthAxis = true;
        _sideDetector.IsZDepthAxis = isZDepthAxis;
    }

    private bool isTargetStandingInTheSameAxis(Vector3 aimedTargetPosition)
    {
        var _acceptableDifference = 0.5f;
        if (isZDepthAxis)
            return (Mathf.Abs(transform.position.z - aimedTargetPosition.z) < _acceptableDifference);
        else
            return (Mathf.Abs(transform.position.x - aimedTargetPosition.x) < _acceptableDifference);
    }

    public void SetIsPlayerPointingTarget(GameObject aimedTarget, Vector3 aimingPoint)
    {
        if (aimedTarget != null && isTargetStandingInTheSameAxis(aimedTarget.transform.position))
        {
            aimedTargetPosition = new Vector3(aimedTarget.transform.position.x, aimingPoint.y, aimedTarget.transform.position.z);
            _isPointingAtTarget = true;
            _aimingLine.SetActive(true);
        }
        else
        {
            aimedTargetPosition = null;
            _isPointingAtTarget = false;
            _aimingLine.SetActive(false);
        }
    }

    private void Update()
    {
        if (_aimingLine == null)
            return;
        hits = Physics.RaycastAll(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), Mathf.Infinity, _layerMask); //wsadzic do kamery, dodac interface'y, dodac event na podstawie trafienia
        var isRaycastHitting = (hits.Length > 0);
        if (!isRaycastHitting)
        {
            aimedTargetPosition = null;
            _aimingLine.SetActive(false);
            return;
        }
        else
        {
            SetIsPlayerPointingTarget(hits[0].collider.gameObject, hits[0].point);
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward) * hits[0].distance, Color.yellow);
        }

        if (_isPointingAtTarget)
        {
            //calculate middle position between player and middle of the screen
            _playerPos = transform.position;
            var _xAxisMiddlePoint = (aimedTargetPosition.Value.x - _playerPos.x) / 2;
            var _zAxisMiddlePoint = (aimedTargetPosition.Value.z - _playerPos.z) / 2;
            var _yAxisMiddlePoint = (aimedTargetPosition.Value.y - _playerPos.y) / 2;

            //
            //  Rotate aiming line and set _aimingLine between target and player
            //

            var _direction = (transform.position - aimedTargetPosition.Value);
            var scaleY = Vector3.Distance(aimedTargetPosition.Value, _playerPos) * 0.5f;
            _aimingLineShaderRenderer.material.SetFloat("_distanceScaleValue", scaleY);

            if (isZDepthAxis)
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
            //
            //  Getting collisions on the aiming line way
            //
        }
    }
}