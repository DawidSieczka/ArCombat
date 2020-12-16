using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    private void Awake()
    {
        CallInits();
    }

    void CallInits()
    {
        //FindObjectOfType<Spawner>().Init();
        FindObjectOfType<SideDetector>().Init();
    }
}
