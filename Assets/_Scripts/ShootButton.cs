using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShootButton : PressButton
{
    /// <summary>
    /// TODO Not implemented Shoot button but instead set reset button for tests 
    /// </summary>
    /// 
    [HideInInspector] public bool Shoot { get; set; }

    [HideInInspector]
    public bool reset
    {
        get { return Pressed;}
    }
}