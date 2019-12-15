using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class JumpButton : PressButton{
    [HideInInspector]
    public bool Jump{
        get { return Pressed; }
    }

    public override bool Pressed{ 
        get => base.Pressed; 
        set{
            OnJumped(this, new EventArgs());
            base.Pressed = value;
        }
    }

    public event EventHandler OnJumped;
}
