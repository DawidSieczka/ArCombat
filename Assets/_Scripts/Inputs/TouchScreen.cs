using UnityEngine;
using UnityEngine.Events;

public class TouchScreen : MonoBehaviour
{
    private Vector2 _startTouch;
    private Vector2 _currentTouch;
    private PlayerHorizontalMovement _playerMovement;

    [HideInInspector]
    public UnityEvent OnJumped;
    private Character_Animator _animator;
    public void GetPlayerHorizontalMovement(GameObject player)
    {
        _playerMovement = player.GetComponent<PlayerHorizontalMovement>();
    }

    private void Update()
    {
        if (_playerMovement == null)
        {
            Debug.LogWarning("Player Horizontal Movement is not attached yet.");
            return;
        }

        if (!MenuOption.IsMenuOptionOpen)
        {
            SetTouchPositions();

#if UNITY_EDITOR
            var horizontalInput = Input.GetAxisRaw("Horizontal");
            MoveHorizontal(horizontalInput * 4);
            //Anim Horizontal 
            if (Input.GetKeyDown(KeyCode.Space))
            {
             //Anim Jump
                Jump();
            }
#endif
        }
    }

    private void CalculateAngle()
    {
        var touchVectorDifference = _currentTouch - _startTouch;
        var xDistance = _currentTouch.x - _startTouch.x;
        var yDistance = _currentTouch.y - _startTouch.y;
        var angle = Mathf.Atan2(touchVectorDifference.x, touchVectorDifference.y) * Mathf.Rad2Deg;
        if (angle < 0)
        {
            angle *= -1;
        }

        InvokeActionBasedOnAnglesRules(angle, xDistance, yDistance);
    }

    private void InvokeActionBasedOnAnglesRules(float angle, float xDistance, float yDistance)
    {
        var minSlide = 40;
        var isXMinTouchDistance = Mathf.Abs(xDistance) > minSlide;
        var isYMinTouchDistance = Mathf.Abs(yDistance) > minSlide;

        if (angle < 20 && isYMinTouchDistance)
        {
            Jump();
            //Anim Jump
        }
        else if (angle > 20 && angle < 60 && isXMinTouchDistance)
        {
            MoveHorizontal(xDistance, minSlide);
            Jump();
            //Anim Jump
        }
        else if (angle > 60 && angle < 125 && isXMinTouchDistance)
        {
            MoveHorizontal(xDistance, minSlide);
            

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
        _playerMovement.Move(input);
    }

    private void MoveHorizontal(float xDistance, int minSlide)
    {
        var direction = 0;
        if (xDistance < 0)
            direction = -1;
        else
            direction = 1;

        if (Mathf.Abs(xDistance) > (minSlide * 2))
            _playerMovement.Move(direction * 2);
        else
            _playerMovement.Move(direction);
    }

    private void SetTouchPositions()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _startTouch = touch.position;
                return;
            }

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
            //Turn Off animation
        }
    }
    public void InitCharacterAnimator()
    {
        _animator = GameObject.FindGameObjectWithTag(Tag.Player.ToString()).GetComponentInChildren<Character_Animator>();
      
    }
    
}