using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreBoardSorter : MonoBehaviour
{
    private void OnEnable()
    {
        var scores = GetComponentsInChildren<PlayerScores>().OrderBy(x => x.Deaths - x.Kills).ToList();

        for (int i = 0; i < scores.Count(); i++)
        {
            scores[i].transform.SetSiblingIndex(i);
        }
    }

}
