using UnityEngine;

public class MenuOption : MonoBehaviour
{
    public static bool IsMenuOptionOpen;

    private void OnEnable()
    {
        IsMenuOptionOpen = true;
    }

    private void OnDisable()
    {
        IsMenuOptionOpen = false;
    }
}