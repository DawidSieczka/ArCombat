using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOption : MonoBehaviour
{
    public static bool isMenuOptionOpen;
    private void OnEnable()
    {
        isMenuOptionOpen = true;
    }

    private void OnDisable()
    {
        isMenuOptionOpen = false;
    }


}
