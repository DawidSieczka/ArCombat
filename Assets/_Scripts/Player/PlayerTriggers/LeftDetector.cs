using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftDetector : TriggerSideDetector{
    
    void Update(){
        _pillarManager.IsAngleForLeftRot = isSetAngleOfRotate;
    }
}
