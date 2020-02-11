using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour{
    ButtonEvent _buttonEvent;
    Rigidbody _rb;
    public GameObject ObjectOfZeroPosition;

    void Start(){
        _buttonEvent = FindObjectOfType<ButtonEvent>();
        _rb = GetComponent<Rigidbody>();
        _buttonEvent.OnRestarted.AddListener(ResetPosition);
    }

    void ResetPosition(){
        transform.localPosition = ObjectOfZeroPosition.transform.position;
        _rb.velocity = Vector3.zero;

        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
