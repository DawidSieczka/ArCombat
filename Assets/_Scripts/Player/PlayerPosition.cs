using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerPosition : MonoBehaviour {
    
    PillarManager _pillarManager;
    PillarDetector _pillarDetector;
    
    void Awake() {
        _pillarManager = FindObjectOfType<PillarManager>();
        _pillarDetector = FindObjectOfType<PillarDetector>();
    }

    void Update() {
        //it help with movement player with rotated pillar and detach pillar from pillar
        SwitchPlayerParent();
    }

    private void SwitchPlayerParent() {
        if (_pillarManager.EnabledRotation)
            StopPlayerPosition();
        else 
            transform.parent = null;
    }

    void StopPlayerPosition(){
        var pillar = _pillarDetector.GetTheNearestPillar();
        transform.parent = pillar.transform;
    }
}
