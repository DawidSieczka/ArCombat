using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerSideDetector : MonoBehaviour{
    
    protected PillarManager _pillarManager;
    protected bool isSetAngleOfRotate;
    
    void Awake(){
        _pillarManager = FindObjectOfType<PillarManager>();
    }

    void OnTriggerStay(Collider other){
        if (other.gameObject.CompareTag(Tag.PillarTag.ToString())){
            isSetAngleOfRotate = true;
        }
    }

    void OnTriggerExit(Collider other){
        if (other.gameObject.CompareTag(Tag.PillarTag.ToString())){
            isSetAngleOfRotate = false;
        }
    }
}
