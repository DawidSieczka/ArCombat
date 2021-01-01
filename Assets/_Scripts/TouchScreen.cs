using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TouchScreen : MonoBehaviour
{
    private Vector2 _startTouch;
    private Vector2 _currentTouch;
    private PlayerHorizontalMovement playerMovement;
    
    public TMPro.TextMeshProUGUI starttext;
    public TMPro.TextMeshProUGUI curenttext;
    public TMPro.TextMeshProUGUI angletext;
    public TMPro.TextMeshProUGUI distancetext;

    [HideInInspector]
    public UnityEvent OnJumped;
    
    public void GetPlayerHorizontalMovement(GameObject player)
    {
        playerMovement = player.GetComponent<PlayerHorizontalMovement>();
    }
    void Update()
    {
        if (playerMovement == null)
        {
            Debug.LogWarning("Player Horizontal Movement is not attached yet.");
            return;
        }

        if (!MenuOption.isMenuOptionOpen)
        {
            SetTouchPositions();

#if UNITY_EDITOR
            
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            MoveHorizontal(horizontalInput * 4);
            
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();
#endif
        }
    }

    private void CalculateAngle()
    {
        var touchVectorDifference =  _currentTouch - _startTouch;
        var xDistance = _currentTouch.x - _startTouch.x;
        var yDistance = _currentTouch.y - _startTouch.y;
        var angle = Mathf.Atan2(touchVectorDifference.x, touchVectorDifference.y) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle *= -1;
        }
        
        angletext.text = angle.ToString();
        distancetext.text = yDistance.ToString();
        InvokeActionBasedOnAnglesRules(angle, xDistance, yDistance);
    }
    
    private void InvokeActionBasedOnAnglesRules(float angle,float xDistance, float yDistance)
    {
        var minSlide = 40;
        var isXMinTouchDistance = Mathf.Abs(xDistance) > minSlide;
        var isYMinTouchDistance = Mathf.Abs(yDistance) > minSlide;

        if (angle < 20 && isYMinTouchDistance)//+ distance
        {
            Jump();
        }
        else if(angle > 20 && angle < 60 && isXMinTouchDistance)//+ distance
        {
            //go jump and right or left
            MoveHorizontal(xDistance, minSlide);
            Jump();
        }
        else if(angle > 60 && angle < 125 && isXMinTouchDistance)
        {
            //go right or left
            MoveHorizontal(xDistance, minSlide);
        }
        else
        {
            //postac kuca ? cos mozna tu ustawić tj zmiana broni albo rzut granatem ...
            // ?????
        }
    }

    [HideInInspector]
    public void Jump()
    {
        OnJumped.Invoke();
    }
    private void MoveHorizontal(float input)
    {
        //used in debug
        playerMovement.Move(input);
    }
    private void MoveHorizontal(float xDistance, int minSlide)
    {
        var direction = 0;
        if (xDistance < 0)
            direction = -1;
        else
            direction = 1;

        if (Mathf.Abs(xDistance) > (minSlide * 2))
            playerMovement.Move(direction * 2);
        else
            playerMovement.Move(direction);
    }

    private void SetTouchPositions()
    {
        //sprawdz czy dotkniec jest więcej niż 0. (czyli po prostu czy dotknięto ekran) 
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            //pobierz miejsce pierwszego dotkniecia
            if (touch.phase == TouchPhase.Began)
            {
                _startTouch = touch.position;
                return;
            }

            //Pobieraj (co klatkę) miejsce poruszonego palca.
            if (touch.phase == TouchPhase.Moved)
            {
                _currentTouch = touch.position;
                CalculateAngle();
            }
        }
        else
        {
            _startTouch = Vector2.zero;
            _currentTouch = Vector2.zero;
        }
    }
}
