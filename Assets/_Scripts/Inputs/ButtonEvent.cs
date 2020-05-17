using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEvent : MonoBehaviour{
    [HideInInspector]
    public UnityEvent OnJumped;
    [HideInInspector]
    public UnityEvent OnRestarted;
    [HideInInspector]
    public UnityEvent OnRotated;
    [HideInInspector]
    public UnityEvent OnShoot;

    [HideInInspector]
    public void Jump(){
        OnJumped.Invoke();
    }

    [HideInInspector]
    public void Restart(){
        OnRestarted.Invoke();
    }

    [HideInInspector]
    public void Rotate(){
        OnRotated.Invoke();
    }

    [HideInInspector]
    public void Shoot(){
        OnShoot.Invoke();   
    }
}
