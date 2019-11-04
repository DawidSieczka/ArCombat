using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyButton : MonoBehaviour, IPointerUpHandler,IPointerDownHandler
{
    [HideInInspector] public bool Pressed;

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
    }
}
