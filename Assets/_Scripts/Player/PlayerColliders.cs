using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliders : MonoBehaviour{
    [HideInInspector]
    public bool IsOnEdge;
    [HideInInspector]
    public PlayerJump _playerJump;

    RaycastHit _centerRay;
    int _layerMaskFillar = 1 << 9;
    int _layerMaskGround = 1 << 8;

    [HideInInspector]
    public RaycastHit _hitInfoLeft;
    [HideInInspector]
    public RaycastHit _hitInfoRight;
    
    void Start(){
        _playerJump = GetComponent<PlayerJump>();
    }

    void Update(){ 
        Debug.DrawRay(transform.position,Vector3.forward/4,Color.red);
        CheckPlayerOnPlatform();
        CheckPlayerBeforeFillar();
    }

    void CheckPlayerBeforeFillar(){
        Physics.Raycast(transform.position, Vector3.forward / 4, out _centerRay, _layerMaskFillar);

        if (!_playerJump.IsColliding()){
            IsOnEdge = false;
        }
        else if (_centerRay.collider == null || !_centerRay.collider.CompareTag(Tag.PillarTag.ToString())){
            IsOnEdge = true;
        }
        else{
            IsOnEdge = false;
        }
        Debug.Log($"Is on edge? : {IsOnEdge}");
    }

    void CheckPlayerOnPlatform(){
        var hitGround1 = Physics.Raycast(transform.localPosition + new Vector3(-0.03f, 0, 0), Vector3.down,
            out _hitInfoLeft, .11f, _layerMaskGround);
        var hitGround2 = Physics.Raycast(transform.localPosition + new Vector3(0.03f, 0, 0), Vector3.down,
            out _hitInfoRight, .11f, _layerMaskGround);

        Debug.DrawRay(transform.localPosition + new Vector3(-0.03f, 0, 0), Vector3.down * .11f, Color.red);
        Debug.DrawRay(transform.localPosition + new Vector3(0.03f, 0, 0), Vector3.down * .11f, Color.red);

    }
}
