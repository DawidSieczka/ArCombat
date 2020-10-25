using TMPro;
using UnityEngine;

public class PlayerName : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _inputPlayerName;

    public void OnClick_EnterName()
    {
        var canvasManager = GetComponentInParent<CanvasesManager>();
        canvasManager.JoinServer(_inputPlayerName.text);
    }
}