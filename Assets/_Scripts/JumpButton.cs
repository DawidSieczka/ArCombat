using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpButton : PressButton
{
    [HideInInspector]
    public bool Jump
    {
        get { return Pressed; }
    }
}
