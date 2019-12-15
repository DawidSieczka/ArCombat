using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class ShootButton : PressButton{
    /// <summary>
    /// TODO Not implemented Shoot button but instead set reset button for tests 
    /// </summary>
    [HideInInspector] public bool Shoot { get; set; }

    [HideInInspector]
    public bool reset{
        get { return Pressed;}
    }

    public event EventHandler OnShooted;

    public override bool Pressed{
        get => base.Pressed;
        set{
            OnShooted(this, new EventArgs());
            base.Pressed = value;
        }
    }
}