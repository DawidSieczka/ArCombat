using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDetector : MonoBehaviour
{
    public side CurrentSide = side.front;
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            ChangeSide();
        }
    }
    void ChangeSide()
    {
        if (CurrentSide == side.front)
        {
            CurrentSide = side.back;
        }
        else
        {
            CurrentSide = side.front;
        }
    }
}
public enum side{
    back,
    front
}