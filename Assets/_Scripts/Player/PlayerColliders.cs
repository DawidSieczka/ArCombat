using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliders : MonoBehaviour{
    [HideInInspector]
    public bool IsOnEdge;

    public float arraySpacingFromMiddlePoint = 0.15f;
    int _layerMaskGround = 1 << 8;

    [HideInInspector]
    public RaycastHit _hitInfoLeft;
    [HideInInspector]
    public RaycastHit _hitInfoRight;

    void Update(){ 
        Debug.DrawRay(transform.position,Vector3.forward/4,Color.red);
        CheckPlayerOnPlatform();
    }

    void CheckPlayerOnPlatform(){
        var hitGround1 = Physics.Raycast(transform.localPosition + new Vector3(-arraySpacingFromMiddlePoint, 0, 0), Vector3.down,
            out _hitInfoLeft, 1.1f, _layerMaskGround);
        var hitGround2 = Physics.Raycast(transform.localPosition + new Vector3(arraySpacingFromMiddlePoint, 0, 0), Vector3.down,
            out _hitInfoRight, 1.1f, _layerMaskGround);

        Debug.DrawRay(transform.localPosition + new Vector3(-arraySpacingFromMiddlePoint, 0, 0), Vector3.down * 1.1f, Color.red);
        Debug.DrawRay(transform.localPosition + new Vector3(arraySpacingFromMiddlePoint, 0, 0), Vector3.down * 1.1f, Color.red);

    }
}
