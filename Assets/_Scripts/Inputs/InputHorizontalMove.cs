using UnityEngine;

public class InputHorizontalMove : MonoBehaviour
{
    [HideInInspector]
    public bool ShouldMove;

    [HideInInspector]
    public bool IsRightDirection;

    private void Update()
    {
        if (ShouldMove)
        {
            if (IsRightDirection)
                MoveRight();
            else
                MoveLeft();
        }
    }

    public void OnPointerDown(bool isRightMove)
    {
        IsRightDirection = isRightMove;
        ShouldMove = true;
    }

    public void OnPointerUp()
    {
        ShouldMove = false;
        PlayerHorizontalMovement.Direction = 0;
    }

    public void MoveLeft()
    {
        PlayerHorizontalMovement.Direction = -1;
    }

    public void MoveRight()
    {
        PlayerHorizontalMovement.Direction = 1;
    }
}