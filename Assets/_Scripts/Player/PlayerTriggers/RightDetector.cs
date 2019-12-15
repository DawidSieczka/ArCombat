using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightDetector : TriggerSideDetector{
   
    void Update(){
        _pillarManager.isAngleForRightRot = isSetAngleOfRotate;
    }
}
