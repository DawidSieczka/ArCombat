using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDetector : MonoBehaviour
{
    [HideInInspector]
    public side CurrentSide = side.front;
    GameObject _player;
    Vector3 _playerPos;
    Vector3 _camPos;
    Vector3 _camPlayerDifferent;
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag(Tag.Player.ToString());   
    }
    
    private void Update()
    {
        _playerPos = _player.transform.position;
        _camPos = transform.position;
        _camPlayerDifferent = _camPos - _playerPos;
        var _isZDepthAxis = _player.GetComponent<PlayerAim>().isZDepthAxis;
        if (_isZDepthAxis)
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
            if(_camPlayerDifferent.x < 0)
            {
                CurrentSide = side.front;
            }
            else
            {
                CurrentSide = side.back;

            }
        }
    }
}
public enum side{
    back,
    front
}