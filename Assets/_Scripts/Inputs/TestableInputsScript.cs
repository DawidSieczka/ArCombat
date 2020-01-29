using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestableInputsScript : MonoBehaviour{

    Collider2D _buttonCollider;
    
    public GameObject playerButton;
    Vector2 _buttonTouchArea;
    Vector2 _buttonCenter;
    PlayerJump _playerJump;
    void Awake()
    {
        _buttonCollider = playerButton.GetComponent<Collider2D>();
        _buttonTouchArea = _buttonCollider.bounds.size;
        _buttonCenter = _buttonCollider.bounds.center;
        _playerJump = FindObjectOfType<PlayerJump>();
        ValidateButtonColliderSize();
    }

    private void ValidateButtonColliderSize()
    {
        if (_buttonTouchArea.x != _buttonTouchArea.y)
        {
            Debug.LogError($"Button: {_buttonCollider.name} isn't a square:\nx: {_buttonTouchArea.x}\n y:{_buttonTouchArea.y}");
        }
    }

    private void Update()
    {
        bool hasTouch = Input.touchCount > 0;
        if (hasTouch)
        {
            foreach(var touch in Input.touches)
            {
                try
                { 
                    var touchDistance = Vector2.Distance(touch.position, _buttonCenter);
                
                    if(touchDistance < _buttonTouchArea.x/2)
                    {
                        //ACTION
                        //TODO Ta akcja bedzie abstrakcyjna pozniej. Ze wzgledu na bug wstawiam na chama metode skakania
                        _playerJump.Jump();
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Input has error: {ex.Message}");
                }
            }
        }
    }

    //TODO moglbym tutaj zrobic metode która sobie subskrybuje event, a zaznaczam co ma subskrybowac z poziomu edytora w inspektorze.
    //Kod będzie bardzo słabo ze sobą związany

}
