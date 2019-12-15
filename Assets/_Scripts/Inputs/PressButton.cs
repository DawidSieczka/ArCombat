using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PressButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{
    public virtual bool Pressed { get; set; }

    public void OnPointerUp(PointerEventData eventData){
        Pressed = false;
    }

    public void OnPointerDown(PointerEventData eventData){
        Pressed = true;
    }
}
