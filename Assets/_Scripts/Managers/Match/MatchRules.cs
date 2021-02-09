using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MatchRules : MonoBehaviour
{
    [SerializeField]
    private GameObject _wonPanel;
    public void EnableWonScreen(string winnerName)
    {
        _wonPanel.GetComponentInChildren<TextMeshProUGUI>().text = $"Player {winnerName} won the match!";
        _wonPanel.SetActive(true);
    }
}
