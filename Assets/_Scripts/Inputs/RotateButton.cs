using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateButton : PressButton{
    [HideInInspector]
    public bool Rotate{
        get { return Pressed; }
    }

    public event EventHandler OnRotated;

    public override bool Pressed{
        get => base.Pressed;
        set{
            OnRotated(this, new EventArgs());
            base.Pressed = value;
        }
    }

}

