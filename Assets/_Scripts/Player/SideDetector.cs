using UnityEngine;

public class SideDetector : MonoBehaviour
{
    [HideInInspector]
    public side CurrentSide = side.front;

    [HideInInspector]
    public bool IsZDepthAxis;

    private GameObject _camera;
    private Vector3 _playerPos;
    private Vector3 _camPos;
    private Vector3 _camPlayerDifferent;

    public void Awake()
    {
        _camera = GameObject.FindGameObjectWithTag(Tag.MainCamera.ToString());
    }

    private void Update()
    {
        if (_camera == null)
        {
            Debug.LogError("Side detector can't find the main camera");
            return;
        }

        SetLocations();
        SetCurrentPlayerSideForCamera();
    }

    public void ChangeDepthAxis()
    {
        IsZDepthAxis = !IsZDepthAxis;
    }

    private void SetCurrentPlayerSideForCamera()
    {
        if (IsZDepthAxis)
        {
            if (_camPlayerDifferent.x < 0)
            {
                CurrentSide = side.front;
            }
            else
            {
                CurrentSide = side.back;
            }
        }
        else
        {
            if (_camPlayerDifferent.z < 0)
            {
                CurrentSide = side.back;
            }
            else
            {
                CurrentSide = side.front;
            }
        }
    }

    private void SetLocations()
    {
        _playerPos = transform.position;
        _camPos = _camera.transform.position;
        _camPlayerDifferent = _playerPos - _camPos;
    }
}

public enum side
{
    back,
    front
}