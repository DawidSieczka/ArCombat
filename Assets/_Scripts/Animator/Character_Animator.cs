using Boo.Lang;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Animator : MonoBehaviour
{
    Animator newAnimator;
    public void HorizontalAnimator()
    {

        newAnimator.SetBool("anim", true);

    }
 
    void Start()
    {
       
        newAnimator = GetComponent<Animator>();
        FindObjectOfType<TouchScreen>().InitCharacterAnimator();

    }


    void Update()
    {
      
     
        

    }
}
