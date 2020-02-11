using UnityEngine;

public class InputHorizontalMove : MonoBehaviour
{

    [HideInInspector]
    public bool shouldMove;
    [HideInInspector]
    public bool isRightDirection;

    void Update()
    {
        if (shouldMove)
        {
            if (isRightDirection)
                MoveRight();
            else
                MoveLeft();
        }
    }
    public void OnPointerDown(bool isRightMove)
    {
        isRightDirection = isRightMove;
        shouldMove = true;
    }
    public void OnPointerUp()
    {
        shouldMove = false;
        PlayerHorizontalMovement.direction = 0;

    }
    public void MoveLeft()
    {
        PlayerHorizontalMovement.direction = -1;
    }
    public void MoveRight()
    {
        PlayerHorizontalMovement.direction = 1;
    }
}