using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class ResetScene : MonoBehaviour{
    ShootButton _shootButton;
    Rigidbody _rb;
    public GameObject ObjectOfZeroPosition;

    void Start(){
        _shootButton = FindObjectOfType<ShootButton>();
        _rb = GetComponent<Rigidbody>();
        _shootButton.OnShooted += ResetPosition;
    }

    void ResetPosition(object s,EventArgs e){
        transform.localPosition = ObjectOfZeroPosition.transform.position;
        _rb.velocity = Vector3.zero;

        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
    }
}
