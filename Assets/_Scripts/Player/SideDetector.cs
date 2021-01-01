using UnityEngine;

public class SideDetector : MonoBehaviour
{
    [HideInInspector]
    public side CurrentSide = side.front;

    [HideInInspector]
    public bool IsZDepthAxis;

    private GameObject _player;
    private Vector3 _playerPos;
    private Vector3 _camPos;
    private Vector3 _camPlayerDifferent;

    public void Init()
    {
        _player = GameObject.FindGameObjectWithTag(Tag.Player.ToString());
    }

    private void Update()
    {
        if (_player == null)
        {
            Debug.LogError("Side detector can't find the player");
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
            if (_camPlayerDifferent.z < 0)
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
            if (_camPlayerDifferent.x < 0)
            {
                CurrentSide = side.front;
            }
            else
            {
                CurrentSide = side.back;
            }
        }
    }

    private void SetLocations()
    {
        _playerPos = _player.transform.position;
        _camPos = transform.position;
        _camPlayerDifferent = _camPos - _playerPos;
    }
}

public enum side
{
    back,
    front
}