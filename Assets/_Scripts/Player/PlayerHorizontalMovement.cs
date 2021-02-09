using Photon.Pun;
using UnityEngine;

public class PlayerHorizontalMovement : MonoBehaviourPun
{
    public float SpeedMultiplayer;
    public static int Direction = 1;
    public Camera ArCam;
    private SideDetector _sideDetector;
    private Rigidbody _rb;

    [SerializeField] [Range(0, 1)] private float _lerpConstant;

    private void Start()
    {
        InitIfDoesNotExist();
    }

    private void InitIfDoesNotExist()
    {
        if (photonView.IsMine && (_sideDetector || _rb == null))
        {
            _sideDetector = FindObjectOfType<SideDetector>();
            _rb = GetComponent<Rigidbody>();
        }
    }

    private void CheckCameraAndPlayerDirection()
    {
        InitIfDoesNotExist();
        if (_sideDetector.CurrentSide == side.back)
            Direction = -1;
        else
            Direction = 1;
    }

    public void Move(float playerSpeed)
    {
        if (base.photonView.IsMine)
        {
            CheckCameraAndPlayerDirection();
            transform.position += GetVectorDepthDirection() * SpeedMultiplayer * Direction * playerSpeed * Time.deltaTime;
        }
    }

    private Vector3 GetVectorDepthDirection()
    {
        if (_sideDetector.IsZDepthAxis)
            return Vector3.forward;
        else
            return Vector3.right;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts.Length > 0 && base.photonView.IsMine)
        {
            _rb.velocity = Vector3.zero;
        }
    }
}