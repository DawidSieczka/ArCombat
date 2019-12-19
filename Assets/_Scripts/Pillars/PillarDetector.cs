using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarDetector : MonoBehaviour{
   
    GameObject[] _pillars;
    GameObject _theNearestPillar;
    GameObject _player;

    void Start(){
        _pillars = GameObject.FindGameObjectsWithTag(Tag.PillarTag.ToString());
        _player = GameObject.FindGameObjectWithTag(Tag.Player.ToString());
    }

    public GameObject GetTheNearestPillar(){
        var previousDistance = float.MaxValue;

        //Amount of Pillars is about 10-30 (not too much)
        foreach (var pillar in _pillars){
            var distPlayerPillar = Vector3.Distance(pillar.transform.position, _player.transform.position);

            if (distPlayerPillar < previousDistance){
                previousDistance = distPlayerPillar;
                _theNearestPillar = pillar;
            }
        }
        return _theNearestPillar;
    }
}
