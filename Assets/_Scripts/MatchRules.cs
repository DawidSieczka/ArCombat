using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MatchRules : MonoBehaviour
{
    [SerializeField]
    private GameObject WonPanel;
    public void EnableWonScreen(string winnerName)
    {
        WonPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"Player {winnerName} won the match!";
        WonPanel.SetActive(true);
    }
}
