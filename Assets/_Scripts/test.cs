using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour,IPooledObject
{
    public void OnObjectDestroy()
    {
        Debug.Log("dziala destr");
    }

    public void OnObjectSpawn()
    {
        Debug.Log("dziala spawn");

    }

}
